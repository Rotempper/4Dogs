import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CitysService {

  public readonly baseApiUrl= 'https://localhost:44344/api/Citys'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async GetAllCitys(){ // שליפת ערים
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

}

