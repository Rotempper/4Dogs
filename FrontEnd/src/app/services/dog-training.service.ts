import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DogTraining } from '../model/DogTrainingModel';

@Injectable({
  providedIn: 'root'
})
export class DogTrainingService {

  public readonly baseApiUrl= 'https://localhost:44344/api/DogTraining'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async GetAllDogTraining(){ // שליפת כל המשתמשים
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  async AddDogTraining(order_training: DogTraining):Promise<any>{
    return this.http.post(this.baseApiUrl , order_training).toPromise<any>();
  }

  async GetDogTrainingByUserid(userid:number){
    return this.http.get(this.baseApiUrl+"/GetDogTrainingServicesId/"+userid).toPromise<any>();
  }
}
