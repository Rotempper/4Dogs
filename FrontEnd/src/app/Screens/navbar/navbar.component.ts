import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DialogLogoutComponent } from 'src/app/dialog-logout/dialog-logout.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public token = this.AutService.ShareToken;
  public auth = "";
  public role = this.AutService.GetSetRole;

  constructor(private router:Router,public AutService: AuthenticationService,
              public dialog: MatDialog){}

  onHomePage(){
    this.router.navigate(['/**']);
  }

  async ngOnInit(): Promise<void> {
    this.auth = await this.AutService.ShareToken;
  }

  async onLogin(){
  this.router.navigate(['/Login']);
  }

  onCreateUser(){
    this.router.navigate(['/CreateUser']);
  }

  onAbout(){
    this.router.navigate(['/About']);
  }

  onOrders(){
    this.router.navigate(['/Orders']);
  }

  onTheDiary(){
    this.router.navigate(['/TheDiary']);
  }

  onAdmin(){
    this.router.navigate(['/Admin']);
  }

  onAdminOrderManagement(){
    this.router.navigate(['/admin-order-management']);
  }

  onUserData(){
    this.router.navigate(['/user-data']);
  }

  onAddUser(){
    this.router.navigate(['/add-users']);
  }

  async Logout(){
    this.auth = await this.AutService.ShareToken;

    if(this.auth != '{}')
    {
      this.openDialog();
      window.sessionStorage.clear();
      this.router.navigate(['/Home']).then(() => {
        window.location.reload();
      });
    }
  }

  openDialog(){
    this.dialog.open(DialogLogoutComponent);
  }
}
