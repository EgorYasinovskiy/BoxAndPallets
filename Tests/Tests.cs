using BoxAndPallets;

namespace Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void VolumeOfBoxIsCorrect()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 5, 7, 10);
			Assert.AreEqual(box.GetVolume(), 3 * 5 * 7);
		}

		[Test]
		public void VolumeOfPalleteIsCorrect()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 5, 7, 10);
			var box2 = new Box(new DateOnly(2022, 06, 20), 1, 11, 5, 7, 10);
			var pallet = new Pallet(1, 20, 20, 1);
			pallet.TryAddBox(box);
			pallet.TryAddBox(box2);
			Assert.AreEqual(pallet.GetVolume(), 400 + box.GetVolume() + box2.GetVolume());
		}

		[Test]
		public void CanAddBoxWhenMesuaresAreEqual()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 3, 3, 10);
			var pallet = new Pallet(1, 3, 3, 3);
			Assert.IsTrue(pallet.TryAddBox(box));
		}
		[Test]
		public void CanAddBoxWhenPalletIsLower()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 3, 10, 10);
			var pallet = new Pallet(1, 3, 3, 3);
			Assert.IsTrue(pallet.TryAddBox(box));
		}
		[Test]
		public void CantAddBoxWhenBoxIsWider()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 4, 3, 3, 10);
			var pallet = new Pallet(1, 3, 3, 3);
			Assert.IsFalse(pallet.TryAddBox(box));

		}
		[Test]
		public void CantAddBoxWhenBoxIsLonger()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 4, 3, 10);
			var pallet = new Pallet(1, 3, 3, 3);
			Assert.IsFalse(pallet.TryAddBox(box));
		}
		[Test]
		public void SumWeightIsCorrect()
		{
			var box = new Box(new DateOnly(2022, 06, 20), 1, 3, 5, 7, 35);
			var box2 = new Box(new DateOnly(2022, 06, 20), 1, 11, 5, 7, 65);
			var pallet = new Pallet(1, 100, 100, 100);
			pallet.TryAddBox(box);
			pallet.TryAddBox(box2);
			Assert.AreEqual(pallet.Weight, 130);
		}

	}
}