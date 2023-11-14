import { UserData } from './../../model/userData';
import { User } from 'src/app/model/userModel';
import { LoginModel } from '../../model/Login.Model';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  public Loginobj:LoginModel={};
  public auth :UserData = {};
  public UserNameObj:User={};
  public returnUrl = '';
  public error = '';
  public firstName='';

  constructor(private router: Router,private AutService: AuthenticationService,
              private route: ActivatedRoute,private formBuilder: FormBuilder,
              private UserHttp:UserService){}

  ngOnInit(){
    this.loginForm = this.formBuilder.group({
      email: new FormControl('',[Validators.required , Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')]),
      password: ['', Validators.required]
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/**';
  }

  get f() { return this.loginForm.controls; }

  // שליפת משתמש
  async getUserNameById(){
    this.UserNameObj = await this.UserHttp.getUserNameById(Number(this.AutService.GetSetUserId));
    console.log(this.UserNameObj.firstName);
    this.AutService.GetSetUserName = this.UserNameObj.firstName;
  }

  // בדיקת התחברות לאתר
  async onSubmit(){
    try{
      this.Loginobj.email=this.f.email.value; // השמה
      this.Loginobj.password=this.f.password.value;
      this.auth = await this.AutService.getauth(this.Loginobj);
      this.AutService.ShareToken = this.auth.token; // שמירת token
      this.AutService.GetSetUserId = this.auth.id;
      this.AutService.GetSetRole = this.auth.role;

      if(this.auth != null){
        this.router.navigate([this.returnUrl]);
        this.router.navigate(['/Home']).then(() => {
          window.location.reload();
        });
        this.getUserNameById();
        this.firstName = this.AutService.GetSetUserName;
      }
    }
    catch{
      this.error = "מייל או סיסמא לא תקינים.";
    }
  }
}
