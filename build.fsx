#!dotnet fsi

#r "netstandard"
#r "nuget: MSBuild.StructuredLogger"
#r "nuget: Fake.Core"
#r "nuget: Fake.Core.Target"
#r "nuget: Fake.DotNet.Cli"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO
open Fake.IO.Globbing.Operators

// Boilerplate
System.Environment.GetCommandLineArgs()
|> Array.skip 2 // skip fsi.exe; build.fsx
|> Array.toList
|> Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Context.RuntimeContext.Fake
|> Context.setExecutionContext

open Fake.DotNet

let dotnet cmd =
  Printf.kprintf (fun arg ->
    let res = DotNet.exec id cmd arg

    if not res.OK then
      let msg = res.Messages |> String.concat "\n"

      failwithf "Failed to run 'dotnet %s %s' due to: %A" cmd arg msg
  )

let formatTargets =
  !! "./*.fs"
  ++ "Test/**/*.fs"
  ++ "build.fsx"
  -- "**/obj/**/*.fs"
  -- "**/bin/**/*.fs"

Target.create
  "Format"
  (fun _ ->
    formatTargets
    |> String.concat " "
    |> dotnet "fantomas" "%s"
  )

Target.create
  "Format.Check"
  (fun _ ->
    formatTargets
    |> String.concat " "
    |> dotnet "fantomas" "--check %s"
  )

Target.runOrList ()
