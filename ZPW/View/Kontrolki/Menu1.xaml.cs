using System; 
using System.Windows; 
using System.Windows.Controls; 
 
namespace ZPW.View.Kontrolki {
   /// <summary>
      /// Interaction logic for MyUserControl.xaml 
   /// </summary> 
	
   public partial class Menu1 : UserControl 
   { 
	
      public Menu1() { 
         InitializeComponent(); 
      }  
		
#region Grupuj
		private void Grupuj_Click(object sender, RoutedEventArgs e)
		{
         ContextMenu cm = this.FindName("cmGrupuj") as ContextMenu;
			cm.PlacementTarget = sender as Button;
			cm.IsOpen = true;
		}
#endregion //Grupuj


#region Sortuj
		private void Sortuj_Click(object sender, RoutedEventArgs e)
		{
         ContextMenu cm = this.FindName("cmSortuj") as ContextMenu;
			cm.PlacementTarget = sender as Button;
			cm.IsOpen = true;
		}
#endregion //Sortuj

#region Sortuj
		private void Filtruj_Click(object sender, RoutedEventArgs e)
		{
         ContextMenu cm = this.FindName("cmFiltruj") as ContextMenu;
			cm.PlacementTarget = sender as Button;
			cm.IsOpen = true;
		}
#endregion //Sortuj
   } 
}