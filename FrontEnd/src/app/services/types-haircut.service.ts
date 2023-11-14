import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TypesHaircut } from '../model/TypesHaircutModel';

@Injectable({
  providedIn: 'root'
})
export class TypesHaircutService {

  public readonly baseApiUrl= 'https://localhost:44344/api/TypesHaircut'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async AddTypesHaircut(data:TypesHaircut):Promise<any>{
    return this.http.post(this.baseApiUrl , data).toPromise<any>();
  }

  async GetAllTypesHaircut(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  async getTypesHaircutByBarberShopId(barberShopId:number){
    return this.http.get(this.baseApiUrl+"/getTypesHaircutByBarberShopId/" + barberShopId).toPromise<any>();
  }

  async getTypesHaircutById2(id:number){
    return this.http.get(this.baseApiUrl+"/getTypesHaircutById2/" + id).toPromise<any>();
  }

  async getTypesHaircutById1(id:number){
    return this.http.get(this.baseApiUrl+"/getTypesHaircutById1/" + id).toPromise<any>();
  }

  async updatTypesHaircut( id:number|undefined,dataTypesHaircut:TypesHaircut,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataTypesHaircut, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  //מחיקת תספורת
  async DeleteTypesHaircut(id:number|undefined,token:any){
    return this.http.delete(this.baseApiUrl + "/" + id, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}
