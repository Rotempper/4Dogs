import { BarberShop } from './../../model/barberShopModel';
import { Component, OnInit } from '@angular/core';
import { Dogowner } from 'src/app/model/dogownerModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { DogTrainingService } from 'src/app/services/dog-training.service';
import { TrainingOrderModel } from 'src/app/model/TrainingOrderModel';
import { Training } from 'src/app/model/trainingModel';
import { TrainingService } from 'src/app/services/training.service';
import { Citys } from 'src/app/model/citysModel';
import { CitysService } from 'src/app/services/citys.service';
import { HaircutOrderModel } from 'src/app/model/HaircutOrderModel';
import { HaircutsService } from 'src/app/services/haircuts.service';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { LodgningOrderModel } from 'src/app/model/LodgningOrderModel';
import { LodgingService } from 'src/app/services/lodging.service';
import { PensionService } from 'src/app/services/pension.service';
import { Pension } from 'src/app/model/pensionModel';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

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


  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');
  public DatepresentDogTraining: Array<TrainingOrderModel> = [];
  public DateDateArcivDogTraining: Array<TrainingOrderModel> = [];
  public DatepresentHaircut: Array<HaircutOrderModel> = [];
  public DateArcivHaircut: Array<HaircutOrderModel> = [];
  public DatepresentLodging: Array<LodgningOrderModel> = [];
  public DateDateArcivLodging: Array<LodgningOrderModel> = [];

  constructor(private AutService:AuthenticationService,private DogOwnerHttp:DogOwnerService,
               private DogTrainingHttp:DogTrainingService,private TrainingHttp:TrainingService,
               private CitysHttp:CitysService,private HaircutHttp:HaircutsService,
               private BarberShopHttp:BarberShopService,private LodgingHttp: LodgingService,
               private PensionHttp: PensionService,private AuthHttp: AuthenticationService){}

  async ngOnInit(){
    this.auth = await this.AutService.ShareToken;
    await this.data();
    this.GetCitys();
    this.GetTrainingData();
    this.GetBarberShopData();
    this.GetPensionData();
  };

  // userId  לפי dogownerId שולף את
  async getOwnerId(){
    this.dogownerobj = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(this.AutService.GetSetUserId),this.token);
  }

  // dogowner Id שולף את הפרטים של ההזמנה לפי
  async DogTraininOrderData(){
    this.TrainingOrderDataObj = await this.DogTrainingHttp.GetDogTrainingByUserid(Number(this.dogownerobj.id));
    console.log(this.TrainingOrderDataObj);


  // מיון תאריכים
  for(let i =0; i <this.TrainingOrderDataObj.length; i++)
  {
    if(this.currentDate <= formatDate(new Date(String(this.TrainingOrderDataObj[i].startDate) ), 'yyyy/MM/dd', 'en'))
    {
     this.DatepresentDogTraining.push(this.TrainingOrderDataObj[i]);
    }
    else{
      this.DateDateArcivDogTraining.push(this.TrainingOrderDataObj[i]);
    }
  }
}

  // שליפת תספורות
  async HaircutOrderData(){  // dogowner Id שולף את הפרטים של ההזמנה לפי
    this.HaircutOrderDataObj = await this.HaircutHttp.GetHaircutId(Number(this.dogownerobj.id));
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
    this.LodgingOrderDataObj = await this.LodgingHttp.GetLodgingId(Number(this.dogownerobj.id));
    console.log(this.LodgingOrderDataObj);

   // מיון תאריכים
   for(let i =0; i <this.LodgingOrderDataObj.length; i++)
   {
     if(this.currentDate >= formatDate(new Date(String(this.LodgingOrderDataObj[i].startDate) ), 'yyyy/MM/dd', 'en'))
     {
      this.DateDateArcivLodging.push(this.LodgingOrderDataObj[i]);
     }
     else{
       this.DatepresentLodging.push(this.LodgingOrderDataObj[i]);
     }
   }
  }

  // אם יש טוקן
  async data(){
    this.auth = await this.AutService.ShareToken;
    if (this.auth != '{}'){
      await this.getOwnerId();
      await this.DogTraininOrderData();
      await this.HaircutOrderData();
      await this.LodgingOrderData();
    }
  }

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
