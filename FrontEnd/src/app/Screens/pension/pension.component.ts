import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import * as moment from 'moment';
import { Citys } from 'src/app/model/citysModel';
import { Dogowner } from 'src/app/model/dogownerModel';
import { Pension } from 'src/app/model/pensionModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CitysService } from 'src/app/services/citys.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { LodgingService } from 'src/app/services/lodging.service';
import { PensionService } from 'src/app/services/pension.service';

@Component({
  selector: 'app-pension',
  templateUrl: './pension.component.html',
  styleUrls: ['./pension.component.css']
})
export class PensionComponent implements OnInit {

  public role = this.AuthHttp.GetSetRole;
  public token = this.AuthHttp.ShareToken;
  public allPension: Array<Pension> = [];
  public allCitys: Array<Citys> = [];
  public idPension:number | undefined;
  public dogownerobj:Dogowner={};
  public auth = "";
  public ok ='';
  public errorcurrentDate ='';
  public error = "לא ניתן לבצע הזמנה ללא התחברות לאתר.";
  public errorRole = "אינך יכול/ה לבצע הזמנה כמנהל/ת או כבעל/ת עסק.";
  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');


  constructor(private PensionHttp:PensionService,private CitysHttp:CitysService,
              private AutService:AuthenticationService,private DogOwnerHttp:DogOwnerService,
              private LodgingHttp:LodgingService,private AuthHttp: AuthenticationService,
              private dateAdapter: DateAdapter<Date>){this.dateAdapter.setLocale('il');}

  // פנסיון
  public PensionOrder = new FormGroup({
    userId: new FormControl('',[Validators.required]),
    nameBusiness: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    pricePerForNight: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required])
  });

  // לינה
  public PensionLodgingOrder = new FormGroup({
    dogOwnerId: new FormControl('',[Validators.required]),
    StartDate: new FormControl('',[Validators.required]),
    EndDate: new FormControl('',[Validators.required]),
    PensionId: new FormControl('',[Validators.required]),
  });

  async ngOnInit(): Promise<void> {
    this.auth = await this.AutService.ShareToken; // בודק אם יש טוקן כדי לדעת אם לפתוח את האפשרות לבצע הזמנה
    await this.GetPension();
    await this.GetCitys();
  }

  // שליפת פנסיונים
  async GetPension(){
  this.allPension = await this.PensionHttp.GetAllUsers_Pension();
  }

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
    console.log(this.allCitys);
  };

  // שליפת מס יחודי
  async getOwnerId(){
    this.dogownerobj = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(this.AutService.GetSetUserId),this.token);
  }

  // הזמנת לינה
  async LodgingOrder()
  {
    this.auth = await this.AutService.ShareToken;
    if(this.auth != '{}')
    {
      await this.getOwnerId();
      this.PensionLodgingOrder.value.dogOwnerId = this.dogownerobj.id;
      this.PensionLodgingOrder.value.PensionId = this.idPension;

      // שישמור יום אחד קדימה- להצגה נכונה
      this.PensionLodgingOrder.value.StartDate.setDate(this.PensionLodgingOrder.value.StartDate.getDate() + 1);
      this.PensionLodgingOrder.value.EndDate.setDate(this.PensionLodgingOrder.value.EndDate.getDate() + 1);
      // שלא יהיה ניתן לקבוע תור אם התאריך כבר עבר
      let DateformatUser = moment(this.PensionLodgingOrder.value.StartDate).format('yyyy/MM/DD');

      if(DateformatUser < this.currentDate)
      {
        this.errorcurrentDate = 'לא ניתן לבחור תאריך זה';
        this.PensionLodgingOrder.reset();
        return;
      }
      this.errorcurrentDate='';
      await this.LodgingHttp.AddLodging(this.PensionLodgingOrder.value);
      this.ok = "התור נקבע בהצלחה!";
      this.PensionLodgingOrder.reset();
    }
  }

  //  לוח שנה - חסימת ימים
  public myFilterDate = (date: Date |any): boolean => {
    const day = (date || new Date()).getDay();
    return day !== 5 && day !=6;
  };
}
