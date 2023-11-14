import { Dogowner } from './../model/dogownerModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DogOwnerService {

  // ניווט לכתובת של משתמש ב .net
  public readonly baseApiUrl= 'https://localhost:44344/api/DogOwner';
  constructor(private http:HttpClient){}

  // יצירת משתמש בעל כלב
  async AddDogowner(user_dogowner:Dogowner):Promise<any>{
    return this.http.post(this.baseApiUrl , user_dogowner).toPromise<any>();
  }

  //  שליפת כל המשתמשים בעלי כלבים
  async GetAllUsers_Dogowner(token:any){
    return this.http.get(this.baseApiUrl,{
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  //  שליפת בעל כלב ע"י מס מזהה
  async GetDogOwnerByUserid(userid:number , token:any){
    return this.http.get(this.baseApiUrl+"/GetDogOwnerByUserid/"+userid,{
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  //  עדכון פרטי בעל הכלב
  async updatDogOwner( id:number|undefined,dataDogowner:Dogowner,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataDogowner, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
