using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;

namespace ZPW.ViewModel
{

#region Multile Selection ListBox

public class MultipleSelectionListBox : ListBox
{
    public static readonly DependencyProperty BindableSelectedItemsProperty =
        DependencyProperty.Register("BindableSelectedItems",
            typeof(List<string>), typeof(MultipleSelectionListBox),
            new FrameworkPropertyMetadata(default(List<string>),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged));

    public List<string> BindableSelectedItems
    {
        get => (List<string>)GetValue(BindableSelectedItemsProperty);
        set => SetValue(BindableSelectedItemsProperty, value);
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        BindableSelectedItems = SelectedItems.Cast<string>().ToList();
    }

    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MultipleSelectionListBox listBox)
            listBox.SetSelectedItems(listBox.BindableSelectedItems);
    }
}


#endregion //Multiple Selection Listbox

}