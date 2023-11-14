import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { User } from '../model/userModel';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  // ניווט לכתובת של משתמש ב .net
  public readonly baseApiUrl= 'https://localhost:44344/api/Users';
  constructor(private http:HttpClient){}

  // שליפת משתמש יחיד ע"י מס יחודי
  async GetSingleUser(id:number | undefined,token:any){
    return this.http.get(this.baseApiUrl +"/" + id, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  // שליפת כל המשתמשים
  async GetAllUsers(){
    return this.http.get(this.baseApiUrl).toPromise<any>();
  }

  // token
  getAll(){
    return this.http.get<User[]>(this.baseApiUrl);
  }

  // יצירת משתמש
  async AddUser(user:User):Promise<any>{
    return this.http.post(this.baseApiUrl , user).toPromise<any>();
  }

  //שליפת שם משתמש ע"י מס מזהה
  async getUserNameById(userid:number){
    return this.http.get(this.baseApiUrl+"/getUserNameById/" + userid).toPromise<any>();
  }

  //שליפת מס מזהה ע"י מייל
  async GetUseridByEmail(email:string){
    return this.http.get(this.baseApiUrl+"/getUserIdByEmail/" + email).toPromise<any>();
  }

  //בדיקה האם המייל כבר קיים במערכת
  async GetCheckEmailInSql(email:string){
    return this.http.get(this.baseApiUrl+"/checkEmailInSql/" + email).toPromise<any>();
  }

  //מחיקת משתמש - בעל כלב
  async DeleteAllUsers(id:number|undefined,token:any){
    return this.http.delete(this.baseApiUrl + "/" + id, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }

  //// עדכון פרטי משתמש
  async updateUser( id:number|undefined,dataUser:User,token:any):Promise<any>{
    return this.http.put(this.baseApiUrl + "/" + id,dataUser, {
      headers: new HttpHeaders().set('Authorization', token),
    }).toPromise<any>();
  }
}

