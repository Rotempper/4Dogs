import { CitysService } from './../../services/citys.service';
import { UserService } from './../../services/user.service';
import { ShopsService } from './../../services/shops.service';
import { Shops } from './../../model/shopsModel';
import { Dogowner } from './../../model/dogownerModel';
import { Component, OnInit} from '@angular/core';
import { Pension } from 'src/app/model/pensionModel';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Training } from 'src/app/model/trainingModel';
import { PensionService } from 'src/app/services/pension.service';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { TrainingService } from 'src/app/services/training.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { User } from 'src/app/model/userModel';
import { Citys } from 'src/app/model/citysModel';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit{

  public trainingCustomer:Array<Training> = [];
  public BarberShopCustomer:Array<BarberShop> = [];
  public PensionCustomer:Array<Pension> = [];
  public dogownerCustomer: Array<Dogowner> = [];
  public shopCustomer: Array<Shops> = [];
  public UserCustomer: Array<User> = [];
  public allCitys: Array<Citys> = [];
  public currentShop:Shops = {};
  public openCloseEditAdd = 1;
  public token = this.AuthHttp.ShareToken;

  constructor(private DogOwnerHttp:DogOwnerService,private TrainingHttp:TrainingService,
              private BarberShopHttp:BarberShopService,private PensionHttp: PensionService,
              private ShopHttp:ShopsService,private UserHttp: UserService,
              private CitysHttp: CitysService,private router: Router,
              private AuthHttp: AuthenticationService){}

  ngOnInit(): void {
    this.GetTrainingData();
    this.GetBarberShopData();
    this.GetPensionData();
    this.GetShopData();
    this.GetUserData();
    this.GetDogOwnerData();
    this.GetCitys();
  }

  async GetTrainingData():Promise<any>{ // שליפת נתונים - אילוף
    this.trainingCustomer = await this.TrainingHttp.GetAllUsers_Training();
  };

  async GetBarberShopData():Promise<any>{  // שליפת נתונים - מספרות
    this.BarberShopCustomer = await this.BarberShopHttp.GetAllUsers_BarberShop();
  };

  async GetPensionData():Promise<any>{ // שליפת נתונים - פנסיון
    this.PensionCustomer = await this.PensionHttp.GetAllUsers_Pension();
  };

  async GetShopData():Promise<any>{ // שליפת נתונים - חנויות
    this.shopCustomer = await this.ShopHttp.GetAllShops(this.token);
  };

  async GetUserData():Promise<any>{ // שליפת נתונים - משתמש
    this.UserCustomer = await this.UserHttp.GetAllUsers();
  };

  async GetDogOwnerData():Promise<any>{ // שליפת נתונים - בעל הכלב
    this.dogownerCustomer = await this.DogOwnerHttp.GetAllUsers_Dogowner(this.token);
  };

  async GetCitys():Promise<any>{  // שליפת ערים
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // מחיקת משתמש בעל חנות
  async DeleteShopData (id:number|undefined):Promise<any>{
    await this.ShopHttp.DeleteUserShops(id,this.token);
    this.router.navigate(['/Admin']).then(() => {
      window.location.reload();
    });
  }

  // מחיקת משתמש בעל כלב
  async DeleteAllUsersData (id:number|undefined):Promise<any>{
    await this.UserHttp.DeleteAllUsers(id,this.token);
    this.router.navigate(['/Admin']).then(() => {
      window.location.reload();
    });
  }
}

