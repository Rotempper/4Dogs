import { Dogowner } from './../../model/dogownerModel';
import { TypesHaircutService } from 'src/app/services/types-haircut.service';
import { CitysService } from './../../services/citys.service';
import { BarberShopService } from './../../services/barber-shop.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Citys } from 'src/app/model/citysModel';
import { HaircutsService } from 'src/app/services/haircuts.service';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { DateAdapter } from '@angular/material/core';
import {formatDate} from '@angular/common';
import * as moment from 'moment';

@Component({
  selector: 'app-barbershop',
  templateUrl:'./barbershop.component.html',
  styleUrls: ['./barbershop.component.css']
})
export class BarbershopComponent implements OnInit {

  public role = this.AuthHttp.GetSetRole;
  public token = this.AutService.ShareToken;
  public allBarbershops: Array<BarberShop> = [];
  public allCitys: Array<Citys> = [];
  public allTypesHaircut: Array<TypesHaircut> = [];
  public idbarbershop: number |undefined;
  public dogownerobj:Dogowner={};
  public ok = '';
  public auth = "";
  public formInvalid='';
  public errorTime = '';
  public errorcurrentDate ='';
  public error = "לא ניתן לבצע הזמנה ללא התחברות לאתר.";
  public errorRole = "אינך יכול/ה לבצע הזמנה כמנהל/ת או כבעל/ת עסק.";
  public currentDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');
  public options = [{time: "8:00"}]

  // תספורת
  public HaircutsOrder = new FormGroup({
    dogOwnerId: new FormControl('',[Validators.required]),
    ddate: new FormControl('',[Validators.required]),
    hhour: new FormControl('',[Validators.required]),
    typesHaircutId: new FormControl('',[Validators.required]),
    barbershopId: new FormControl('',[Validators.required]),
  });

  constructor(private BarberShopHttp : BarberShopService,private CitysHttp: CitysService,
              private HaircutsHttp : HaircutsService,private TypesHaircutHttp : TypesHaircutService,
              private AutService:AuthenticationService,private DogOwnerHttp:DogOwnerService,
              private AuthHttp:AuthenticationService,private dateAdapter: DateAdapter<Date>){this.dateAdapter.setLocale('il');}

  async ngOnInit(): Promise<void> {
    this.auth = await this.AutService.ShareToken; // בודק אם יש תוקן כדי לדעת אם לפתוח את האפשרות לבצע הזמנה
    await this.GetBarberShop();
    await this.GetCitys();
    await this.GetTypesHaircut();
    await this.StartTime();
  }

  async getOwnerId(){
    this.dogownerobj = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(this.AutService.GetSetUserId),this.token);
  }

  // שליפת מספרות
  async GetBarberShop():Promise<any>{
    this.allBarbershops = await this.BarberShopHttp.GetAllUsers_BarberShop();
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // הגבלת שעה
  public StartTime(){
    for(let i = 9; i <= 17; i++){
      this.options.push({time: i + ":00"})
    }
  }

  // יצירת הזמנה - תספורת
  async creatHaircuts():Promise<any>{
    this.auth = await this.AutService.ShareToken;
    if(this.auth != '{}')
    {
      await this.getOwnerId();
      this.HaircutsOrder.value.dogOwnerId = this.dogownerobj.id;
      this.HaircutsOrder.value.barbershopId =  this.idbarbershop;

      // בחירת תאריך נוכחי מדויק
      this.HaircutsOrder.value.ddate.setDate(this.HaircutsOrder.value.ddate.getDate() + 1);

      let hourNotFree = await this.HaircutsHttp.GetcheckTime( this.HaircutsOrder.value.hhour,this.HaircutsOrder.value.ddate,this.idbarbershop);
      if(hourNotFree){
        this.errorTime = 'השעה תפוסה, אנא בחר שעה אחרת';
        this.HaircutsOrder.reset();
        return;
      }

      // * שלא יהיה ניתן לקבוע תור אם התאריך כבר עבר !
      let DateformatUser = moment(this.HaircutsOrder.value.ddate).format('yyyy/MM/DD');
      if(DateformatUser < this.currentDate){
        this.errorcurrentDate = 'לא ניתן לבחור תאריך זה';
        return;
      }
      this.errorcurrentDate='';
      await this.HaircutsHttp.AddHaircuts(this.HaircutsOrder.value);
      this.ok = "התור נקבע בהצלחה!";
      this.HaircutsOrder.reset();
    }
  }

  // שליפת סוגי תספורות
  async GetTypesHaircut():Promise<any>{
    this.allTypesHaircut = await this.TypesHaircutHttp.GetAllTypesHaircut();
    console.log(this.allTypesHaircut);
  };

  // לוח שנה - לא ניתן לקבוע בשישי שבת
  public myFilterDate = (date: Date |null): boolean => {
    const day = (date || new Date()).getDay();
    return day !== 5 && day !=6;
 };
}
