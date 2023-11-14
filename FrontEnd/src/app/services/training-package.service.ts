import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrainingPackage } from '../model/TrainingPackageModel';

@Injectable({
  providedIn: 'root'
})
export class TrainingPackageService {

  public readonly baseApiUrl= 'https://localhost:44344/api/TrainingPackage'; // ניווט לכתובת של משתמש ב .net

  constructor(private http:HttpClient) { }

  async AddTrainingPackage(data:TrainingPackage):Promise<any>{
    return this.http.post(this.baseApiUrl , data).toPromise<any>();
  }

  async GetAllTrainingPackage(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  async GetSingleTrainingPackage(id:number | undefined){ // שליפת משתמש יחיד ע"י מס יחודי
    return this.http.get(this.baseApiUrl +"/" + id).toPromise<any>();
  }

  async getTrainingPackageByTrainingId(TrainingPackageId:number){
    return this.http.get(this.baseApiUrl+"/getTrainingPackageByTrainingId/" + TrainingPackageId).toPromise<any>();
  }

  async updatTrainingPackage( id:number|undefined,dataTrainingPackage:TrainingPackage,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataTrainingPackage, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  //מחיקת חבילת אילוף
  async DeleteTrainingPackage(id:number|undefined,token:any){
    return this.http.delete(this.baseApiUrl + "/" + id, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

}
