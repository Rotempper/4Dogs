import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BarberShop } from '../model/barberShopModel';

@Injectable({
  providedIn: 'root'
})
export class BarberShopService {

  // ניווט לכתובת של משתמש ב .net
  public readonly baseApiUrl= 'https://localhost:44344/api/BarberShop';
  constructor(private http:HttpClient){}

  // יצירת משתמש בעל מספרה
  async AddBarberShop(user_barberShop:BarberShop):Promise<any>{
    return this.http.post(this.baseApiUrl , user_barberShop).toPromise<any>();
  }

  // שליפת מספרה יחיד ע"י מס מזהה
  async GetSingleBarberShop(id:number | undefined){
    return this.http.get(this.baseApiUrl +"/" + id).toPromise<any>();
  }

  //  שליפת בעלי מספרות
  async GetAllUsers_BarberShop(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  //  שליפת מס מזהה מספרה ע"י מס מזהה משתמש
  async GetBarberShopIdByUserId(UserId:number){
    return this.http.get(this.baseApiUrl+"/getBarberShopIdByUserId/" + UserId).toPromise<any>();
  }

  // שליפת אובייקט מספרה ע"י מס מזהה משתמש
  async getBarberShopByUserIdObj(UserId:number){
    return this.http.get(this.baseApiUrl+"/getBarberShopByUserIdObj/" + UserId).toPromise<any>();
  }

  // עדכון מספרה יחיד ע"י מס מזהה
  async updatBarberShop( id:number|undefined,dataBarberShop:BarberShop,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataBarberShop, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
