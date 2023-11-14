import { formatDate } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { MatAccordion } from '@angular/material/expansion';
import * as moment from 'moment';
import { Citys } from 'src/app/model/citysModel';
import { Dogowner } from 'src/app/model/dogownerModel';
import { DogTraining } from 'src/app/model/DogTrainingModel';
import { Training } from 'src/app/model/trainingModel';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CitysService } from 'src/app/services/citys.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { DogTrainingService } from 'src/app/services/dog-training.service';
import { TrainingPackageService } from 'src/app/services/training-package.service';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css']
})
export class TrainingComponent implements OnInit {

  @ViewChild('picker') picker: any;
  @ViewChild(MatAccordion) accordion: MatAccordion | undefined;
    range = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });

  public token = this.AuthHttp.ShareToken;
  public role = this.AuthHttp.GetSetRole;
  public allTrainingPackage: Array<TrainingPackage> = [];
  public allDogTraining: Array<DogTraining> = [];
  public allTraining :Array<Training> = [];
  public allCitys: Array<Citys> = [];
  public dogownerobj:Dogowner={};
  public idTraining: number |undefined;
  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');
  public errorRole = "אינך יכול/ה לבצע הזמנה כמנהל/ת או כבעל/ת עסק.";
  public error = "לא ניתן לבצע הזמנה ללא התחברות לאתר.";
  public ok = '';
  public auth = "";
  public errorcurrentDate ='';

  //מאלף כלבים
  public DogTrainingOrder = new FormGroup({
    dogOwnerId: new FormControl('',[Validators.required]),
    StartDate: new FormControl('',[Validators.required]),
    EndDate: new FormControl('',[Validators.required]),
    trainingId: new FormControl('',[Validators.required]),
    packagId: new FormControl('',[Validators.required]),
  });

  constructor(private TrainingHttp:TrainingService,private CitysHttp:CitysService,
              private TrainingPackageHttp:TrainingPackageService,private DogTrainingHttp:DogTrainingService,
              private AutService:AuthenticationService,private DogOwnerHttp:DogOwnerService,
              private AuthHttp: AuthenticationService,private dateAdapter: DateAdapter<Date>){this.dateAdapter.setLocale('il');}

  async ngOnInit(): Promise<void>{
    this.auth = await this.AutService.ShareToken;
    await this.GetTraining();
    await this.GetCitys();
    await this.GetTrainingPackage();
  }

  // שליפת מאלפים
  async GetTraining():Promise<any>{
    this.allTraining = await this.TrainingHttp.GetAllUsers_Training();
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
    console.log(this.allCitys);
  };

  // שליפת חבילות אילוף
  async GetTrainingPackage():Promise<any>{
    this.allTrainingPackage = await this.TrainingPackageHttp.GetAllTrainingPackage();
    console.log(this.allTrainingPackage);
  };

  // הזמנת חבילת אילוף ממאלף
  async GetTDogTraining():Promise<any>{
    this.allDogTraining = await this.DogTrainingHttp.GetAllDogTraining();
    console.log(this.allDogTraining);
  };

  async getOwnerId(){
    this.dogownerobj = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(this.AutService.GetSetUserId),this.token);
  }

  async creatDogTraining(){

    this.auth = await this.AutService.ShareToken;
    if(this.auth != '{}')
    {
      await this.getOwnerId();
      this.DogTrainingOrder.value.dogOwnerId = this.dogownerobj.id;
      this.DogTrainingOrder.value.trainingId =  this.idTraining;

      // שישמור יום אחד קדימה- להצגה נכונה
      this.DogTrainingOrder.value.StartDate.setDate(this.DogTrainingOrder.value.StartDate.getDate() + 1);
      this.DogTrainingOrder.value.EndDate.setDate(this.DogTrainingOrder.value.EndDate.getDate() + 1);
      // שלא יהיה ניתן לקבוע תור אם התאריך כבר עבר
      let DateformatUser = moment(this.DogTrainingOrder.value.StartDate).format('yyyy/MM/DD');

      if(DateformatUser < this.currentDate)
      {
        this.errorcurrentDate = 'לא ניתן לבחור תאריך זה';
        this.DogTrainingOrder.reset();
        return;
      }
      this.errorcurrentDate='';
      await this.DogTrainingHttp.AddDogTraining(this.DogTrainingOrder.value);
      this.ok = "התור נקבע בהצלחה!";
      this.DogTrainingOrder.reset();
    }
  }

  //  לוח שנה - חסימת ימים
  public myFilterDate = (date: Date |any): boolean => {
    const day = (date || new Date()).getDay();
    return day !== 5 && day !=6;
  };
}

