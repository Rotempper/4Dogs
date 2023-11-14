import { TrainingService } from './../../services/training.service';
import { Lodging } from './../../model/LodgingModel';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { Dogowner } from 'src/app/model/dogownerModel';
import { AuthenticationService } from './../../services/authentication.service';
import { HaircutsService } from './../../services/haircuts.service';
import { Haircuts } from './../../model/haircutsModel';
import { Component, OnInit} from '@angular/core';
import { BarberShop } from 'src/app/model/barberShopModel';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { TypesHaircutService } from 'src/app/services/types-haircut.service';
import { Pension } from 'src/app/model/pensionModel';
import { PensionService } from 'src/app/services/pension.service';
import { LodgingService } from 'src/app/services/lodging.service';
import { DogTraining } from 'src/app/model/DogTrainingModel';
import { Training } from 'src/app/model/trainingModel';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { TrainingPackageService } from 'src/app/services/training-package.service';
import { DogTrainingService } from 'src/app/services/dog-training.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-the-diary',
  templateUrl: './the-diary.component.html',
  styleUrls: ['./the-diary.component.css']
})

export class TheDiaryComponent implements OnInit {

  public role = this.AuthHttp.GetSetRole;
  public token = this.AuthHttp.ShareToken;
  public allTypesHaircut: Array<TypesHaircut> = [];
  public allHaircut: Array<Haircuts> = [];
  public allDogowner: Array<Dogowner> = [];
  public currentBarberShopId :BarberShop= {};
  public allPensions: Array<Pension> = [];
  public allLodgning: Array<Lodging> = [];
  public currentPensionId :Pension= {};
  public allDogTraining: Array<DogTraining> = [];
  public currentTrainingId :Training= {};
  public allTrainingPackage: Array<TrainingPackage> = [];
  public Datepresent: Array<Haircuts> = [];
  public DateArciv: Array<Haircuts> = [];
  public DatepresentLodging: Array<Lodging> = [];
  public DateDateArcivLodging: Array<Lodging> = [];
  public DatepresentDogTraining: Array<DogTraining> = [];
  public DateDateArcivDogTraining: Array<DogTraining> = [];
  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');

  constructor(private BarberShopHttp:BarberShopService,private TypesHaircutHttp:TypesHaircutService,
              private HaircutHttp: HaircutsService,private AuthHttp:AuthenticationService,
              private DogOwnerHttp:DogOwnerService,private PensionHttp:PensionService,
              private LodgingHttp:LodgingService, private TrainingHttp:TrainingService,
              private TrainingPackageHttp:TrainingPackageService,private DogTrainingHttp:DogTrainingService){}

   ngOnInit(){
    this.GetHaircut();
    this.GetTypesHaircut();
    this.CurrentBarberShopId();
    this.GetDogOwnerData();
    this.GetPension();
    this.GetLodgning();
    this.CurrentPensionId();
    this.CurrentTrainingId();
    this.GetTrainingPackage();
    this.GetTDogTraining();
  }

  // שליפת נתונים - בעל הכלב
  async GetDogOwnerData():Promise<any>{
    this.allDogowner = await this.DogOwnerHttp.GetAllUsers_Dogowner(this.token);
  };

  // שליפת סוגי תספורות
  async GetTypesHaircut():Promise<any>{
    this.allTypesHaircut = await this.TypesHaircutHttp.GetAllTypesHaircut();
  };

  // שליפת הזמנת תספורת
  async GetHaircut():Promise<any>{

    this.allHaircut = await this.HaircutHttp.GetAllHaircuts();
    // מיון תאריכים
    for(let i =0; i <this.allHaircut.length; i++)
    {
      if(this.currentDate >= formatDate(new Date(String(this.allHaircut[i].ddate) ), 'yyyy/MM/dd', 'en'))
      {
        this.DateArciv.push(this.allHaircut[i]);
      }
      else{
        this.Datepresent.push(this.allHaircut[i]);
      }
    }
  };

  // שליפת סוגי פנסיונים
  async GetPension():Promise<any>{
    this.allPensions = await this.PensionHttp.GetAllUsers_Pension();
  };

  // שליפת  הזמנת פנסיון/לינה
  async GetLodgning():Promise<any>{

    this.allLodgning = await this.LodgingHttp.GetAllLodging();
    // מיון תאריכים
    for(let i =0; i <this.allLodgning.length; i++)
    {
      if(this.currentDate >= formatDate(new Date(String(this.allLodgning[i].startDate) ), 'yyyy/MM/dd', 'en'))
      {
        this.DateDateArcivLodging.push(this.allLodgning[i]);
      }
      else{
        this.DatepresentLodging.push(this.allLodgning[i]);
      }
    }
  };

  // שליפת חבילות אילוף
  async GetTrainingPackage():Promise<any>{
    this.allTrainingPackage = await this.TrainingPackageHttp.GetAllTrainingPackage();
    console.log(this.allTrainingPackage);
  };

  async GetTDogTraining():Promise<any>{

    this.allDogTraining = await this.DogTrainingHttp.GetAllDogTraining();
    console.log('מאלפים'+this.allDogTraining);
    // מיון תאריכים
    for(let i =0; i <this.allDogTraining.length; i++)
    {
      if(this.currentDate <= formatDate(new Date(String(this.allDogTraining[i].startDate) ), 'yyyy/MM/dd', 'en'))
      {
       this.DatepresentDogTraining.push(this.allDogTraining[i]);
      }
      else{
        this.DateDateArcivDogTraining.push(this.allDogTraining[i]);
      }
    }
  };

  //  מספרה - מס יחודי משתמש נוכחי
  public async CurrentBarberShopId(){
     this.currentBarberShopId = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(this.AuthHttp.GetSetUserId));
  }

  //  פנסיון - מס יחודי משתמש נוכחי
  public async CurrentPensionId(){
    this.currentPensionId = await this.PensionHttp.getPensionByUserId(Number(this.AuthHttp.GetSetUserId));
  }

  //  מאלף - מס יחודי משתמש נוכחי
  public async CurrentTrainingId(){
    this.currentTrainingId = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(this.AuthHttp.GetSetUserId));
  }

  // צריך לשנות לא עובד
  // מחיקת תור במספרה
  // async DeleteAllUsersData (id:number|undefined):Promise<any>{
  //   await this.UserHttp.DeleteAllUsers(id,this.token);
  //   this.router.navigate(['/Admin']).then(() => {
  //     window.location.reload();
  //   });
  // }

}
