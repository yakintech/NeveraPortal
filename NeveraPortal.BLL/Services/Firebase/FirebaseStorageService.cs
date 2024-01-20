using Firebase.Storage;

namespace FirebaseTest.Bussiness
{
	public class FirebaseStorageHelper
	{
		FirebaseStorage firebaseStorage = new FirebaseStorage("neveraportal.appspot.com");// if "gs://" be head of bucket string, it wont work!!!

		public async Task<string> AddImage(Stream file, string blogTitle)
		{
			string fileName = blogTitle + ".jpg";
			var imageUrl = await firebaseStorage
			.Child(fileName)
			.PutAsync(file);

			return imageUrl;

		}

		public async Task DeleteImage(string blogTitle)
		{
			string fileName = blogTitle + ".jpg";
			await firebaseStorage
			.Child(fileName)
			.DeleteAsync();
		}
	}
}
