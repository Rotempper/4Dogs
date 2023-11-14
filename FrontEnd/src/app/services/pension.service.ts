import { Pension } from './../model/pensionModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PensionService {

  // ניווט לכתובת של משתמש ב .net
  public readonly baseApiUrl= 'https://localhost:44344/api/Pension';
  constructor(private http:HttpClient){}

  // יצירת משתמש בעל פנסיון
  async AddPension(user_pension:Pension):Promise<any>{
    return this.http.post(this.baseApiUrl , user_pension).toPromise<any>();
  }

  // שליפת כל המשתמשים בעלי פנסיון
  async GetAllUsers_Pension(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  // שליפת פנסיון יחיד ע"י מס מזהה
  async GetSinglePension(id:number | undefined){
    return this.http.get(this.baseApiUrl +"/" + id).toPromise<any>();
  }

  // שליפת פנסיון ע"י מס מזהה של משתמש
  async getPensionByUserId(UserId:number){
    return this.http.get(this.baseApiUrl+"/getPensionByUserId/" + UserId).toPromise<any>();
  }

  // עדכון פנסיון ע"י מס מזהה
  async updatPension( id:number|undefined,dataPension:Pension,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataPension, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
