module Test.Test1

open NUnit.Framework

open FooBar.Sample

[<Test>]
let ``Identity test`` () =
  let x: FooBar<HKT.Identity> = Test.initFooBar Foo Bar

  let x =
    { foo = HKT.map' ((+) 1) x.foo
      bar = HKT.map' (fun s -> s + "?") x.bar }

  Assert.areEqualFooBar (Foo + 1) (Bar + "?") x

[<Test>]
let ``Option test`` () =
  let x: FooBar<HKT.Identity> =
    Test.initFooBar (Some Foo) None
    |> annotate<FooBar<HKT.Option>>
    |> FooBar.map (Option.defaultValue Foo) (Option.defaultValue Bar)

  Assert.areEqualFooBar Foo Bar x

[<Test>]
let ``Result test`` () =
  let x: FooBar<HKT.Option> =
    Test.initFooBar (Error()) (Ok Bar)
    |> annotate<FooBar<HKT.Result<unit>>>
    |> FooBar.map (Result.toOption) (Result.toOption)

  Assert.areEqualFooBar None (Some Bar) x

[<Test>]
let ``List test`` () =
  let x: FooBar<HKT.List> = Test.initFooBar [ Foo ] []

  let x: FooBar<HKT.Option> =
    x
    |> FooBar.map (List.tryHead) (List.tryHead)

  Assert.areEqualFooBar (Some Foo) None x

[<Test>]
let ``Array test`` () =
  let x: FooBar<HKT.Array> = Test.initFooBar [| Foo |] [||]

  let x: FooBar<HKT.Option> =
    x
    |> FooBar.map (Array.tryHead) (Array.tryHead)

  Assert.areEqualFooBar (Some Foo) None x

[<Test>]
let ``From test`` () =
  let x: FooBar<HKT.From<unit>> = FooBar.init (fun () -> Foo) (fun () -> Bar)

  let x: FooBar<HKT.Identity> =
    x
    |> FooBar.map (fun f -> f >> Some) (fun f -> f >> Some)
    |> annotate<FooBar<HKT.From<unit, HKT.Option>>>
    |> FooBar.map (fun f -> f () |> Option.get) (fun f -> f () |> Option.get)

  Assert.areEqualFooBar Foo Bar x
