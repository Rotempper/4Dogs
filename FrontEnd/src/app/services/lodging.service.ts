import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lodging } from '../model/LodgingModel';

@Injectable({
  providedIn: 'root'
})
export class LodgingService {
  public readonly baseApiUrl= 'https://localhost:44344/api/Lodging';

  constructor(private http:HttpClient) { }

  async AddLodging(user_Lodging:Lodging):Promise<any>{
    return this.http.post(this.baseApiUrl ,user_Lodging).toPromise<any>();
  }

  async GetLodgingId(userid:number){
    return this.http.get(this.baseApiUrl+"/getLodgingByUserId/"+userid).toPromise<any>();
  }

  async GetAllLodging(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }
}
