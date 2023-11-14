import { TrainingPackageService } from 'src/app/services/training-package.service';
import { TypesHaircutService } from 'src/app/services/types-haircut.service';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { TrainingService } from './../../services/training.service';
import { PensionService } from './../../services/pension.service';
import { BarberShopService } from './../../services/barber-shop.service';
import { UserService } from 'src/app/services/user.service';
import { Dogowner } from './../../model/dogownerModel';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { User } from 'src/app/model/userModel';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Pension } from 'src/app/model/pensionModel';
import { Training } from 'src/app/model/trainingModel';

@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.css']
})
export class UserDataComponent implements OnInit {

  public role = this.AuthHttp.GetSetRole;
  public token = this.AuthHttp.ShareToken;
  public currentUser :User = {};
  public currentDogOwner :Dogowner = {};
  public currentBarberShop :BarberShop = {};
  public currentPension :Pension = {};
  public currentTraining :Training = {};
  public currentTypesHaircut:TypesHaircut={};
  public currentTrainingPackage:TrainingPackage={};
  public openCloseEditAdd = 1;
  public saveMode = "";
  mode = '';

  constructor(private userHttp:UserService,private AuthHttp:AuthenticationService,
              private DogOwnerHttp:DogOwnerService,private BarberShopHttp:BarberShopService,
              private PensionHttp:PensionService,private TrainingHttp:TrainingService,
              private TypesHaircutHttp:TypesHaircutService,private TrainingPackageHttp:TrainingPackageService){}

  ngOnInit(): void {
  }

  // שליפת נתוני משתמשים
  async GetUserdata():Promise<any>{
    let userid = this.AuthHttp.GetSetUserId;
    this.currentUser = await this.userHttp.GetSingleUser(Number(userid),this.token);
  };

  // שליפת נתוני בעל כלב
  async GetDogOwnerdata(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentDogOwner = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(userid),this.token);
  };

  // שליפת נתוני בעל מספרה
  async GetBarberShopdata(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentBarberShop = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(userid));
  };

  // שליפת נתוני בעל פנסיון
  async GetPensiondata(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentPension = await this.PensionHttp.getPensionByUserId(Number(userid));
  };

  // שליפת נתוני מאלף כלבים
  async GetTrainingdata(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentTraining = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(userid));
  };

  // שליפת נתוני תספורת
  async GetTypesHaircut(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentBarberShop = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(userid));
    let TypesHaircutid = await this.TypesHaircutHttp.getTypesHaircutById2(Number(this.currentBarberShop.id));
    this.currentTypesHaircut = await this.TypesHaircutHttp.getTypesHaircutById1(Number(TypesHaircutid));
  };

  // שליפת נתוני חבילת אילוף
  async GetTrainingPackage(){
    let userid = this.AuthHttp.GetSetUserId;
    this.currentTraining = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(userid));
    this.currentTrainingPackage = await this.TrainingPackageHttp.getTrainingPackageByTrainingId(Number(this.currentTraining.id));
    console.log(this.currentTrainingPackage);
  };

  // מצב התחברות
  async modeFunc(){

    if(this.saveMode == 'userdata')
    {
      await this.GetUserdata();
      this.openCloseEditAdd = 2;
      return;
    }
    else if(this.saveMode == 'DogOwnerData') // בעל כלב
    {
      await this.GetDogOwnerdata();
      this.openCloseEditAdd = 2;
      return;
    }
    else if(this.saveMode == 'BarberShopData') //מספרה
    {
      await this.GetBarberShopdata();
      this.openCloseEditAdd = 2;
      return;
    }
    else if(this.saveMode == 'PensionData') // פינסיון
    {
      await this.GetPensiondata();
      this.openCloseEditAdd = 2;
      return;
    }
    else if(this.saveMode == 'TypesHaircutData') // סוג תספורת
    {
      await this.GetTypesHaircut();
      this.openCloseEditAdd = 2;
      return;
    }
    else if(this.saveMode == 'TrainingPackageData') // סוג חבילת אילוף
    {
      await this.GetTrainingPackage();
      this.openCloseEditAdd = 2;
      return;
    }
    else //מאלף כלבים
    {
      await this.GetTrainingdata();
      this.openCloseEditAdd = 2;
      return;
    }
  }
}
