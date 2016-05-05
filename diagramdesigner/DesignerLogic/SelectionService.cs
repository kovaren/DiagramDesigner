using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DiagramDesigner
{
    internal class SelectionService
    {
        private DesignerCanvas designerCanvas;

        private List<ISelectable> currentSelection;
        internal List<ISelectable> CurrentSelection
        {
            get
            {
                if (currentSelection == null)
                    currentSelection = new List<ISelectable>();

                return currentSelection;
            }
        }

        //public List<ISelectable> GetSelection()
        //{

        //}

        public SelectionService(DesignerCanvas canvas)
        {
            this.designerCanvas = canvas;
        }

        internal void SelectItem(ISelectable item)
        {
            this.ClearSelection();
            this.AddToSelection(item);


            try
            {
                //update attribute panel
                AttributePanel attributes = new AttributePanel();
                DesignerItem des_item = (DesignerItem)item;
                attributes.Name = des_item.BoundLogicItem.Name;
                attributes.LogicID = des_item.BoundLogicItem.ID;
                attributes.DesignerID = des_item.BoundLogicItem.DesignerID;
                App.Current.MainWindow.Resources["AttributePanelDataSource"] = attributes;
                //show attributes panel when smth is selected
                ((StackPanel)App.Current.MainWindow.FindName("AttributeStackPanel")).Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                
             //   throw;
            }  
        }

        internal void AddToSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = true;
                    CurrentSelection.Add(groupItem);
                }
            }
            else
            {
                item.IsSelected = true;
                CurrentSelection.Add(item);
              
            }
        }

        internal void RemoveFromSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = false;
                    CurrentSelection.Remove(groupItem);
                }
            }
            else
            {
                item.IsSelected = false;
                CurrentSelection.Remove(item);
            }
        }

        internal void ClearSelection()
        {
            //hide attributes panel if selection is cleared
            ((StackPanel)App.Current.MainWindow.FindName("AttributeStackPanel")).Visibility = System.Windows.Visibility.Hidden;
            CurrentSelection.ForEach(item => item.IsSelected = false);
            CurrentSelection.Clear();
        }

        internal void SelectAll()
        {
            ClearSelection();
            CurrentSelection.AddRange(designerCanvas.Children.OfType<ISelectable>());
            CurrentSelection.ForEach(item => item.IsSelected = true);
        }

        internal List<IGroupable> GetGroupMembers(IGroupable item)
        {
            IEnumerable<IGroupable> list = designerCanvas.Children.OfType<IGroupable>();
            IGroupable rootItem = GetRoot(list, item);
            return GetGroupMembers(list, rootItem);
        }

        internal IGroupable GetGroupRoot(IGroupable item)
        {
            IEnumerable<IGroupable> list = designerCanvas.Children.OfType<IGroupable>();
            return GetRoot(list, item);
        }

        private IGroupable GetRoot(IEnumerable<IGroupable> list, IGroupable node)
        {
            if (node == null || node.ParentID == Guid.Empty)
            {
                return node;
            }
            else
            {
                foreach (IGroupable item in list)
                {
                    if (item.ID == node.ParentID)
                    {
                        return GetRoot(list, item);
                    }
                }
                return null;
            }
        }

        private List<IGroupable> GetGroupMembers(IEnumerable<IGroupable> list, IGroupable parent)
        {
            List<IGroupable> groupMembers = new List<IGroupable>();
            groupMembers.Add(parent);

            var children = list.Where(node => node.ParentID == parent.ID);

            foreach (IGroupable child in children)
            {
                groupMembers.AddRange(GetGroupMembers(list, child));
            }

            return groupMembers;
        }
    }
}
