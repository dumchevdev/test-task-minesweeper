using UnityEngine;
using Minesweeper.Runtime.Common;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.UI
{
    public class  CellViewFactory : ICellViewFactory
    {
        private readonly Transform _gridRoot;
        private readonly CellView _cellPrefab;
        private readonly IConfigsProvider _configsProvider;

        public CellViewFactory(UIRoot uiRoot, 
            IConfigsProvider configsProvider, IAssetsProvider assetsProvider)
        {
            _gridRoot = uiRoot.Hud.GridView.Root;
            _configsProvider = configsProvider;
            _cellPrefab = assetsProvider.Load<CellView>(Constants.Resources.CellPrefabPath);
        }
        
        public CellView Create()
        {
            var view = Object.Instantiate(_cellPrefab, _gridRoot);
            view.Initialize(_configsProvider.CellViewConfig);
            
            return view;
        }

        public void Destroy(CellView cellView)
        {
            if (cellView == null) 
                return;
            
            Object.Destroy(cellView.gameObject);
        }
    }
}