using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TreeViewDemo.Models;

namespace TreeViewDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private readonly SourceCache<TreeViewItem, string> _source;

        private readonly ReadOnlyObservableCollection<TreeViewItem> _nodes;
        public ReadOnlyObservableCollection<TreeViewItem> Nodes => _nodes;


        private TreeViewItem selectedNode;
        public TreeViewItem SelectedNode
        {
            get => selectedNode;
            set => this.RaiseAndSetIfChanged(ref selectedNode, value);
        }
        
        private ReactiveCommand<TreeViewItem, Unit> CommandAddFolder { get; }

        public MainWindowViewModel()
        {
            CommandAddFolder = ReactiveCommand.CreateFromTask<TreeViewItem>(OnAddFolderNode);
            
            _source = new SourceCache<TreeViewItem, string>(item => (item.DataContext as Node)?.Id);
            var cancellation = _source
                .Connect()
                .Filter(item => (item.DataContext as Node)?.IsDeleted == false)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _nodes)
                .DisposeMany()
                .Subscribe();
        }

        public async Task InitAsync()
        {
            for (var i = 0; i < 2; i++)
            {
                var nodeTreeViewItem = await CreateNode();
                _source.AddOrUpdate(nodeTreeViewItem);
            }
        }

        private async Task<TreeViewItem> CreateNode()
        {
            await Task.FromResult(true);
            var node = new Node();
            var nodeTreeViewItem = new TreeViewItem
            {
                Header = node,
                DataContext = node,
                Name = node.Name,
                Items = GetChildren()
            };

            return nodeTreeViewItem;
        }

        private IEnumerable<TreeViewItem> GetChildren()
        {
            var children = new List<TreeViewItem>();
            for (var i = 0; i < 1; i++)
            {
                var childNode = new Node();
                var childTreeView = new TreeViewItem
                {
                    Header = childNode,
                    DataContext = childNode,
                    Name = childNode.Name,
                };

                children.Add(childTreeView);
            }

            return children;
        }
        
        private async Task OnAddFolderNode(TreeViewItem treeViewItem)
        {
            var nodeTreeViewItem = await CreateNode();
            _source.AddOrUpdate(nodeTreeViewItem);
        }
    }
}
