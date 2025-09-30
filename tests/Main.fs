module Yzl.Tests.Main
open Expecto

[<EntryPoint>]
let main argv =
  Tests.runTestsInAssemblyWithCLIArgs [ No_Spinner ] argv
