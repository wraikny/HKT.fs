namespace Test

type FooBar<'t> =
  { foo: HKT<'t, int>
    bar: HKT<'t, string> }

module FooBar =
  let inline init foo bar : FooBar<'m> =
    { foo = HKT.pack foo
      bar = HKT.pack bar }

  let inline foo { foo = a } = HKT.unpack a

  let inline bar { bar = a } = HKT.unpack a

  let inline map
    mapFoo
    mapBar
    ({ foo = foo; bar = bar }: FooBar<'m1>)
    : FooBar<'m2> =
    { foo = HKT.map mapFoo foo
      bar = HKT.map mapBar bar }

  module Sample =
    [<Literal>]
    let Foo = 42

    [<Literal>]
    let Bar = "Hello, world!"

    let init () : FooBar<HKT.Identity> = init Foo Bar

[<AutoOpen>]
module Utils =
  let annotate<'a> (a: 'a) : 'a = a

module Assert =
  open NUnit.Framework
  let areEqual<'a> (expected: 'a) (actual: 'a) =
    Assert.AreEqual(expected, actual)

  let inline areEqualFooBar expectedFoo expectedBar fooBar =
    areEqual expectedFoo (FooBar.foo fooBar)
    areEqual expectedBar (FooBar.bar fooBar)
  
module Test =
  let inline initFooBar foo bar =
    let x: FooBar<_> = FooBar.init foo bar
    Assert.areEqualFooBar foo bar x
    x