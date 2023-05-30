(*
MIT License

Copyright (c) 2023 wraikny

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. *)
[<AutoOpen>]
module HKTFs

[<MeasureAnnotatedAbbreviation>]
type HKT<'m, 'a> = obj

#nowarn "42"
[<RequireQualifiedAccess>]
module HKT =
  type F<'m, 'a, 'ma when ('m or 'ma): (static member HKT: ('m * 'a * 'ma) -> unit)> = 'm

  type Identity =
    static member HKT(_: Identity * 'a * 'a) : _ = ()

  type Of<'a> =
    static member HKT(_: Of<'a> * _ * 'a) : _ = ()

  type Option<'t> =
    static member inline HKT(_: Option<'m> * 'a * 'ma option) : _ when F<'m, 'a, 'ma> = ()

  type Option = Option<Identity>

  type ValueOption<'t> =
    static member inline HKT(_: ValueOption<'m> * 'a * 'ma voption) : _ when F<'m, 'a, 'ma> = ()

  type ValueOption = ValueOption<Identity>

  type Result<'e, 't> =
    static member inline HKT(_: Result<'e, 'm> * 'a * Core.Result<'ma, 'e>) : _ when F<'m, 'a, 'ma> = ()

  type Result<'e> = Result<'e, Identity>

  type Seq<'t> =
    static member inline HKT(_: Seq<'m> * 'a * 'ma seq) : _ when F<'m, 'a, 'ma> = ()

  type Seq = Seq<Identity>

  type List<'t> =
    static member inline HKT(_: List<'m> * 'a * 'ma list) : _ when F<'m, 'a, 'ma> = ()

  type List = List<Identity>

  type Array<'t> =
    static member inline HKT(_: Array<'m> * 'a * 'ma[]) : _ when F<'m, 'a, 'ma> = ()

  type Array = Array<Identity>


  type Array2D<'t> =
    static member inline HKT(_: Array2D<'m> * 'a * 'ma[,]) : _ when F<'m, 'a, 'ma> = ()

  type Array2D = Array2D<Identity>


  type Array3D<'t> =
    static member inline HKT(_: Array3D<'m> * 'a * 'ma[,,]) : _ when F<'m, 'a, 'ma> = ()


  type Array3D = Array3D<Identity>

  type Array4D<'t> =
    static member inline HKT(_: Array4D<'m> * 'a * 'ma[,,,]) : _ when F<'m, 'a, 'ma> = ()


  type Array4D = Array4D<Identity>

  type Set<'t> =
    static member inline HKT(_: Set<'m> * 'a * Collections.Set<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Set = Set<Identity>


  type Map<'k, 't when 'k: comparison> =
    static member inline HKT(_: Map<'k, 'm> * 'a * Collections.Map<'k, 'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Map<'k when 'k: comparison> = Map<'k, Identity>


  open System.Collections

  type Dictionary<'k, 't> =
    static member inline HKT(_: Dictionary<'k, 'm> * 'a * Generic.Dictionary<'k, 'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Dictionary<'k> = Dictionary<'k, Identity>


  type ResizeArray<'t> =
    static member inline HKT(_: ResizeArray<'m> * 'a * Collections.ResizeArray<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type ResizeArray = ResizeArray<Identity>


  type ReadOnlyDictionary<'k, 't> =
    static member inline HKT
      (_: ReadOnlyDictionary<'k, 'm> * 'a * Generic.IReadOnlyDictionary<'k, 'ma>)
      : _ when F<'m, 'a, 'ma> =
      ()

  type ReadOnlyDictionary<'k> = ReadOnlyDictionary<'k, Identity>


  type ReadOnlyCollection<'t> =
    static member inline HKT
      (_: ReadOnlyCollection<'m> * 'a * Generic.IReadOnlyCollection<'ma>)
      : _ when F<'m, 'a, 'ma> =
      ()

  type ReadOnlyCollection = ReadOnlyCollection<Identity>


  type Observable<'t> =
    static member inline HKT(_: Observable<'m> * 'a * System.IObservable<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Observable = Observable<Identity>


  type Lazy<'t> =
    static member inline HKT(_: Lazy<'m> * 'a * System.Lazy<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Lazy = Lazy<Identity>


  type Async<'t> =
    static member inline HKT(_: Async<'m> * 'a * Control.Async<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Async = Async<Identity>


  open System.Threading

  type Task<'t> =
    static member inline HKT(_: Task<'m> * 'a * Tasks.Task<'ma>) : _ when F<'m, 'a, 'ma> = ()

  type Task = Task<Identity>


  type From<'i, 't> =
    static member inline HKT(_: From<'i, 'm> * 'a * ('i -> 'ma)) : _ when F<'m, 'a, 'ma> = ()

  type From<'i> = From<'i, Identity>


  type To<'o, 't> =
    static member inline HKT(_: To<'o, 'm> * 'a * ('ma -> 'o)) : _ when F<'m, 'a, 'ma> = ()

  type To<'o> = To<'o, Identity>

  [<RequireQualifiedAccess>]
  module Unsafe =
    let inline retype (x: 'a) : 'b = (# "" x : 'b #)

  let inline pack (x: 'ma) : HKT<'m, 'a> when F<'m, 'a, 'ma> = Unsafe.retype (box x)

  let inline unpack (hkt: HKT<'m, 'a>) : 'ma when F<'m, 'a, 'ma> = unbox hkt

  let inline apply (f: 'ma -> 'b) (hkt: HKT<'m, 'a>) : 'b = hkt |> unpack |> f

  let inline map (f: 'ma1 -> 'ma2) (hkt: HKT<'m1, 'a1>) : HKT<'m2, 'a2> when F<'m1, 'a1, 'ma1> and F<'m2, 'a2, 'ma2> =
    hkt |> unpack |> f |> pack

  let inline map' (f: 'ma -> 'ma) (hkt: HKT<'m, 'a>) : HKT<'m, 'a> = map f hkt

#endnowarn

let inline (|HKT|) hkt = HKT.unpack hkt
