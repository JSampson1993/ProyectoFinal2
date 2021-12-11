using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.AudioRecorder;
using EasyNote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace EasyNote.Controller
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://apis-movil-2-default-rtdb.firebaseio.com/");
        static FirebaseStorage stroageImage = new FirebaseStorage("apis-movil-2.appspot.com");

        public async Task<List<Recordatorio>> GetAllPersons()
        {

            return (await firebase
              .Child("Notas")
              .OnceAsync<Recordatorio>()).Select(item => new Recordatorio
              {
                  NotasDescrip = item.Object.NotasDescrip,
                  notasId = item.Object.notasId
              }).ToList();
        }

        public async Task AddPerson(string name, Byte[] imagen)
        {

            await firebase
              .Child("Notas")
              .PostAsync(new Recordatorio()
              {
                  NotasDescrip = name,
                  notas_Image = imagen
              });
        }

        public async Task AddPersonAu(AudioRecorderService name)
        {

            await firebase
              .Child("Notas")
              .PostAsync(new Notas()
              {
                  recorder = name
              });
        }

        public async Task<Recordatorio> GetPerson(String personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Recordatorio>();
            return allPersons.Where(a => a.notasId == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(string personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Recordatorio>()).Where(a => a.Object.notasId == personId).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Recordatorio() { notasId = personId, NotasDescrip = name });
        }

        public async Task DeletePerson(string personId)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Recordatorio>()).Where(a => a.Object.notasId == personId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }

        public async Task AddNotas(string notasId, string notas_Descrip, string notas_Image, string notas_Audio, string userId)
        {


            //  downloadLink.Text = downloadlink;
            await firebase
              .Child("Notas")
              .PostAsync(new NotasGeneral()
              {
                  notasId = notasId,
                  notas_Descrip = notas_Descrip,
                  notas_Image = notas_Image,
                  notas_Audio = notas_Audio,
                  userId = userId
              });

        }

        public static async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await stroageImage
                .Child("NotasImagenes")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        //Codigo utilizado por Victor

        public ObservableCollection<ENotas> getNotas()
        {
            var itemData = firebase.Child("Recordatorios").AsObservable<ENotas>()
                .AsObservableCollection();

            return itemData;
        }

        public async Task AddNotas(string notasId, string NotasDescrip, string notas_Image)
        {
            ENotas i = new ENotas() { notasId = notasId, notas_Descrip = NotasDescrip, notas_Image = notas_Image };
            await firebase.Child("Recordatorios")
                .PostAsync(i);
        }

        public async Task DeleteNotas(string notasId, string NotasDescrip, string notas_Image)
        {
            var toDeleteItem = (await firebase.Child("Recordatorios")
                .OnceAsync<ENotas>()).FirstOrDefault(a => a.Object.notasId == notasId
                || a.Object.notas_Descrip == NotasDescrip || a.Object.notas_Image == notas_Image);
            await firebase.Child("Recordatorios").Child(toDeleteItem.Key).DeleteAsync();
        }
    }
}
