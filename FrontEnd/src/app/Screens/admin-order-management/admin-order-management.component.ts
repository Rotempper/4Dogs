import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Citys } from 'src/app/model/citysModel';
import { Dogowner } from 'src/app/model/dogownerModel';
import { HaircutOrderModel } from 'src/app/model/HaircutOrderModel';
import { LodgningOrderModel } from 'src/app/model/LodgningOrderModel';
import { Pension } from 'src/app/model/pensionModel';
import { Training } from 'src/app/model/trainingModel';
import { TrainingOrderModel } from 'src/app/model/TrainingOrderModel';
import { User } from 'src/app/model/userModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { CitysService } from 'src/app/services/citys.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { DogTrainingService } from 'src/app/services/dog-training.service';
import { HaircutsService } from 'src/app/services/haircuts.service';
import { LodgingService } from 'src/app/services/lodging.service';
import { PensionService } from 'src/app/services/pension.service';
import { TrainingService } from 'src/app/services/training.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-order-management',
  templateUrl: './admin-order-management.component.html',
  styleUrls: ['./admin-order-management.component.css']
})
export class AdminOrderManagementComponent implements OnInit {


  public DatepresentDogTraining: Array<TrainingOrderModel> = [];
  public token = this.AuthHttp.ShareToken;
  public auth='';
  public error='לא ניתן לצפות בהזמנות ללא התחברות לאתר';
  public dogownerobj:Dogowner={};
  public trainingDetails:Array<Training>=[];
  public TrainingOrderDataObj:Array<TrainingOrderModel>=[];
  public allCitys: Array<Citys> = [];
  public HaircutOrderDataObj:Array<HaircutOrderModel>=[];
  public BarberShopDetails:Array<BarberShop>=[];
  public LodgingOrderDataObj:Array<LodgningOrderModel>=[];
  public PensionDetails:Array<Pension>=[];

  public UserCustomer: Array<User> = [];
  public allDogowner: Array<Dogowner> = [];
  public trainingCustomer:Array<Training> = [];


  public DateArcivDogTraining: Array<TrainingOrderModel> = [];
  public DatepresentHaircut: Array<HaircutOrderModel> = [];
  public DateArcivHaircut: Array<HaircutOrderModel> = [];
  public DatepresentLodging: Array<LodgningOrderModel> = [];
  public DateArcivLodging: Array<LodgningOrderModel> = [];
  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');


  constructor(private AutService:AuthenticationService,private DogOwnerHttp:DogOwnerService,
              private DogTrainingHttp:DogTrainingService,private TrainingHttp:TrainingService,
              private CitysHttp:CitysService,private HaircutHttp:HaircutsService,
              private BarberShopHttp:BarberShopService,private LodgingHttp: LodgingService,
              private PensionHttp: PensionService,private AuthHttp: AuthenticationService,
              private UserHttp: UserService) { }



  async ngOnInit(){
    this.auth = await this.AutService.ShareToken;
    await this.data();
    this.GetCitys();
    this.GetTrainingData();
    this.GetBarberShopData();
    this.GetPensionData();
    this.GetDogOwnerData();
    this.HaircutOrderData();
    this.LodgingOrderData();
  };

   // שליפת נתונים - בעל הכלב
   async GetDogOwnerData():Promise<any>{
    this.allDogowner = await this.DogOwnerHttp.GetAllUsers_Dogowner(this.token);
  };

  // dogowner Id שולף את הפרטים של ההזמנה לפי
  async DogTraininOrderData(){
    this.TrainingOrderDataObj = await this.DogTrainingHttp.GetAllDogTraining();
    console.log(this.TrainingOrderDataObj);

    // מיון תאריכים
    for(let i =0; i <this.TrainingOrderDataObj.length; i++)
    {
      if(this.currentDate <= formatDate(new Date(String(this.TrainingOrderDataObj[i].startDate)), 'yyyy/MM/dd', 'en'))
      {
        this.DatepresentDogTraining.push(this.TrainingOrderDataObj[i]);
      }
      else{
        this.DateArcivDogTraining.push(this.TrainingOrderDataObj[i]);
      }
    }
  }

  // שליפת תספורות
  async HaircutOrderData(){  // dogowner Id שולף את הפרטים של ההזמנה לפי
    this.HaircutOrderDataObj = await this.HaircutHttp.GetAllHaircuts();
    console.log(this.HaircutOrderDataObj);

    // מיון תאריכים
    for(let i =0; i <this.HaircutOrderDataObj.length; i++)
    {
      if(this.currentDate >= formatDate(new Date(String(this.HaircutOrderDataObj[i].ddate) ), 'yyyy/MM/dd', 'en'))
      {
        this.DateArcivHaircut.push(this.HaircutOrderDataObj[i]);
      }
      else{
        this.DatepresentHaircut.push(this.HaircutOrderDataObj[i]);
      }
    }
  }

  // שליפת פנסיונים
  async LodgingOrderData(){  // dogowner Id שולף את הפרטים של ההזמנה לפי
    this.LodgingOrderDataObj = await this.LodgingHttp.GetAllLodging();
    console.log(this.LodgingOrderDataObj);

    // מיון תאריכים
    for(let i =0; i <this.LodgingOrderDataObj.length; i++)
    {
      if(this.currentDate >= formatDate(new Date(String(this.LodgingOrderDataObj[i].startDate) ), 'yyyy/MM/dd', 'en'))
      {
      this.DateArcivLodging.push(this.LodgingOrderDataObj[i]);
      }
      else{
        this.DatepresentLodging.push(this.LodgingOrderDataObj[i]);
      }
    }
  }

  // אם יש טוקן
  async data(){
    this.auth = await this.AutService.ShareToken; //?
    if (this.auth != '{}'){
      await this.DogTraininOrderData();
      // await this.HaircutOrderData();
      // await this.LodgingOrderData();
    }
  }

  // שליפת נתונים - משתמש
  async GetUserData():Promise<any>{
    this.UserCustomer = await this.UserHttp.GetAllUsers();
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // שליפת נתונים - אילוף
  async GetTrainingData():Promise<any>{
    this.trainingDetails = await this.TrainingHttp.GetAllUsers_Training();
  };

  // שליפת נתונים - מספרות
  async GetBarberShopData():Promise<any>{
    this.BarberShopDetails = await this.BarberShopHttp.GetAllUsers_BarberShop();
  };

  // שליפת נתונים - פנסיון
  async GetPensionData():Promise<any>{
    this.PensionDetails = await this.PensionHttp.GetAllUsers_Pension();
  };

}
