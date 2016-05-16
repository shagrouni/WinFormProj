module MainForm 


open System.Drawing
open System.Windows.Forms
  
  type MainForm() as x =
    inherit Form()
    
    let lbl = new Label()
    let txt1 = new TextBox()
    let txt2 = new TextBox()
    let btReverse = new Button()
    
    let menuStrip =
      let menu = new MenuStrip()
        
      let mnuFile = new ToolStripMenuItem("&File")
      let mnuNew = new ToolStripMenuItem("&New")
      let mnuExit = new ToolStripMenuItem("E&xit")

      mnuFile.DropDownItems.Add(mnuNew) |> ignore   // return type of DropDownItems.Add is int so we need to convert it to unit by using ignore function
      mnuFile.DropDownItems.Add(mnuExit) |> ignore
      mnuNew.Click.Add(fun _ -> txt1.Clear()) // return type of Click.Add is unit
      mnuExit.Click.Add(fun _ -> x.Close())
      
      let mnuEdit = new ToolStripMenuItem("&Edit")
      let mnuCopy = new ToolStripMenuItem("&Copy")
      let mnuPaste = new ToolStripMenuItem("&Past")
      
      mnuEdit.DropDownItems.Add(mnuCopy) |> ignore
      mnuEdit.DropDownItems.Add(mnuPaste) |> ignore
      mnuCopy.Click.Add(fun _ -> txt1.Copy() )
      mnuPaste.Click.Add(fun _ -> txt1.Paste() )
      
      menu.Items.Add(mnuFile) |> ignore
      menu.Items.Add(mnuEdit) |> ignore

      menu

    do
      lbl.Location  <- new Point(10, 30) 
      lbl.Size <- Size(640, 30)
      lbl.AutoSize <- true
      lbl.Text <- ""
      lbl.Font <- new Font("Tahoma", 16.0f);

      txt1.Location <- new Point(10, 70) 
      txt1.Size <- Size(240, 140)
      txt1.Multiline <- true
      txt1.AcceptsTab <- true
      txt1.ScrollBars <- ScrollBars.Vertical 
      

      txt1.KeyPress.Add(fun e -> 
                             lbl.Text <- e.KeyChar.ToString()  
                             + " " +  int(e.KeyChar).ToString()
                             )
      txt1.KeyUp.Add(fun e -> 
                             if e.KeyCode.ToString().Length > 1 then
                               lbl.Text <- lbl.Text + " " + e.KeyCode.ToString()
                             )
      txt1.TextChanged.Add(x.Reverse)
      //-----------------------------------     
      txt2.Location <- new Point(260, 70) 
      txt2.Size <- Size(240, 140)
      txt2.Multiline <- true
      txt2.ScrollBars <- ScrollBars.Vertical 
      
      btReverse.Location  <- new Point(10, 220)
      btReverse.TabIndex <- 2
      btReverse.Text <- "Reverse"
      btReverse.Click.Add(x.Reverse)  

      x.ClientSize <- new Size(520, 262);
      x.Text <- "Text Reverse " 
      
      x.Controls.Add(lbl)  
      x.Controls.Add(txt1)  
      x.Controls.Add(txt2)  
      x.Controls.Add(btReverse)
      x.Controls.Add(menuStrip)
           
    member private x.Reverse _ = 
      let rec reverseChars s newText =
        if s = "" then newText
        else  
          reverseChars s.[1..] (s.[0..0] + newText)      

      let s2 = reverseChars txt1.Text ""
      let s2 = s2.Replace("\n\r", "\r\n")
      txt2.Text <- s2



      (* -----------------------------
      let rec reverseChars s newText =
        match s with                                     
        |"" -> newText                                   
        |_  -> reverseChars s.[1..] (s.[0..0] + newText) 
      *)  

      (* -----------------------------
      let reverseChars (s: string) =
        let mutable newText = ""
        for x = s.Length - 1 downto 0 do
          newText <- newText + s.[x..x]  
      
        newText
      *)

      (* -----------------------------
      let s2 = txt1.Text.ToCharArray() |> Array.rev |> String
      *)
      
      
      (* -----------------------------
      let rec reverseChars s newTwxt =
        if s = "" then newTwxt
        else  
          let str =
            match s.[0] with
            |'\n' -> "\r"
            |'\r' -> "\n"
            |_    -> s.[0..0]

          let acc = str + newTwxt 
          reverseChars s.[1..] acc 
      let s2 = reverseChars txt1.Text ""
      *)
      
     
      (* -----------------------------
      let reverseChars (s: string) =  
        let mutable newText = ""
        let mutable c = ""
        for x = s.Length - 1 downto 0 do
          if s.[x] = '\n' then
            c <- "\r"
          elif s.[x] = '\r' then
            c <- "\n"
          else
            c <- s.[x..x]
          newText <- newText + c
        newText
      let s2 = reverseChars txt1.Text
      *)


      (* -----------------------------
      let s2 =
            txt1.Text.ToCharArray() 
            |> Array.rev
            |> Array.map (fun x -> if x = '\r' then '\n' 
                                   elif x = '\n' then '\r' 
                                   else x) 
            |> String
      txt2.Text <- s2
      *)

      (* -----------------------------
      let arr = txt1.Text.ToCharArray()
      let s2 =  Array.foldBack (fun x acc -> acc + x.ToString())  arr ""
      *)
