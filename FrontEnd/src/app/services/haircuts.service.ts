import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Haircuts } from '../model/haircutsModel';

@Injectable({
  providedIn: 'root'
})
export class HaircutsService {

  public readonly baseApiUrl= 'https://localhost:44344/api/Haircuts'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async GetAllHaircuts(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  async GetAllUsers_Haircuts(token:any){ // שליפת כל המשתמשים
    return this.http.get(this.baseApiUrl, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  async AddHaircuts(Haircuts : Haircuts):Promise<any>{
    console.log(Haircuts.ddate);
    return this.http.post(this.baseApiUrl ,  Haircuts).toPromise<any>();
  }

  async GetHaircutId(userid:number){
    return this.http.get(this.baseApiUrl+"/getHaircutsDTOByUserId/"+userid).toPromise<any>();
  }

  async GetcheckTime(time:string, date: Date, id: number|undefined){
    let obj:Haircuts={};
    obj.hhour=time;
    obj.ddate=date;
    obj.id=id;
    return this.http.post(this.baseApiUrl+"/checkTime",obj).toPromise<any>();
  }
}
