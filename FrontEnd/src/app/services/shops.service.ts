import { Shops } from './../model/shopsModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopsService {

  public readonly baseApiUrl= 'https://localhost:44344/api/Shops'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

    //מחיקת משתמש
    async DeleteUserShops(id:number|undefined,token:any){
    return this.http.delete(this.baseApiUrl + "/" + id, {
            headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  async GetAllShops(token:any){ // שליפת כל המשתמשים
    return this.http.get(this.baseApiUrl, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  async updateShop( id:number|undefined,dataShop:Shops,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataShop, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  async AddShop(newShop:Shops,token:any){
    return this.http.post(this.baseApiUrl , newShop, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
