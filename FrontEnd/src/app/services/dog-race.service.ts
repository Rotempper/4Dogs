import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DogRaceService {

  public readonly baseApiUrl= 'https://localhost:44344/api/DogRace'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async GetAllDogRace(){ // שליפת גזעים
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }
}
