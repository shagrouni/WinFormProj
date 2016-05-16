open System
open System.Windows.Forms
open MainForm

module Program =

  let myForm = new MainForm()

  Application.EnableVisualStyles() 
  [<STAThread>]
  [<EntryPoint>]
  Application.Run myForm

  //Application.Run(new MainForm())

 
