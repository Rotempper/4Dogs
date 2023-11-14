import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../model/Login.Model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public readonly baseApiUrl= 'https://localhost:44344/api/Users/auth';

  constructor(private http: HttpClient){}

  async getauth(login:LoginModel){ // בקשה ל login
    return this.http.post(this.baseApiUrl,login).toPromise<any>();
  }

  // sessionStorage שנשמר ב token מביא את ה
  get ShareToken():string{
    return window.sessionStorage.getItem("token") || '{}';
  }
  //sessionStorage ב  token שומר
  set ShareToken(token:any){
    window.sessionStorage.setItem("token",token);
  }

  get GetSetUserId():string{
    return window.sessionStorage.getItem("userId") || '{}';
  }
  set GetSetUserId(userId:any){
   window.sessionStorage.setItem("userId",userId);
  }

  get GetSetUserName():string{
    return window.sessionStorage.getItem("firstName") || '';
  }
  set GetSetUserName(firstName:any){
   window.sessionStorage.setItem("firstName", firstName);
  }

  get GetSetRole():string
  {
    return window.sessionStorage.getItem("Role") || '';
  }
  set GetSetRole(Role:any)
  {
    window.sessionStorage.setItem("Role",Role);
  }

}
