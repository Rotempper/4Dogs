import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Training } from '../model/trainingModel';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  // ניווט לכתובת של משתמש ב .net
  public readonly baseApiUrl= 'https://localhost:44344/api/Training';
  constructor(private http:HttpClient) { }

  // יצירת משתמש מאלף כלבים
  async AddTraining(user_training:Training):Promise<any>{
    return this.http.post(this.baseApiUrl , user_training).toPromise<any>();
  }

  // שליפת כל המשתמשים מאלפי כלבים
  async GetAllUsers_Training(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  // שליפת מאלף כלבים יחיד ע"י מס מזהה
  async GetSingleTraining(id:number | undefined){
    return this.http.get(this.baseApiUrl +"/" + id).toPromise<any>();
  }

  // שליפת מס מזהה מאלף כלבים ע"י מס מזהה משתמש
  async GetTrainingIdByUserId(UserId:number){
    return this.http.get(this.baseApiUrl+"/getTrainingIdByUserId/" + UserId).toPromise<any>();
  }

  // שליפת אובייקט מאלף כלבים ע"י מס מזהה משתמש
  async getTrainingIdByUserIdObj(UserId:number){
    return this.http.get(this.baseApiUrl+"/getTrainingIdByUserIdObj/" + UserId).toPromise<any>();
  }

  // עדכון מאלף כלבים ע"י מס מזהה
  async updatTraining( id:number|undefined,dataTraining:Training,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataTraining, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
