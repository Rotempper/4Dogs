import { TypesHaircutService } from 'src/app/services/types-haircut.service';
import { TrainingService } from './../../services/training.service';
import { PensionService } from 'src/app/services/pension.service';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { Training } from './../../model/trainingModel';
import { Pension } from './../../model/pensionModel';
import { BarberShop } from 'src/app/model/barberShopModel';
import { DogRaceService } from './../../services/dog-race.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Dogowner } from 'src/app/model/dogownerModel';
import { Dograce } from 'src/app/model/dograceModel';
import { User } from 'src/app/model/userModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DogOwnerService } from 'src/app/services/dog-owner.service';
import { UserService } from 'src/app/services/user.service';
import { Citys } from 'src/app/model/citysModel';
import { CitysService } from 'src/app/services/citys.service';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { TrainingPackageService } from 'src/app/services/training-package.service';

@Component({
  selector: 'app-edit-users',
  templateUrl: './edit-users.component.html',
  styleUrls: ['./edit-users.component.css']
})
export class EditUsersComponent implements OnInit {

  public token = this.AuthHttp.ShareToken;
  public passIsOk = false;
  public EmailOk = false;
  public currentUser :User = {};
  public currenDogOwner :Dogowner = {};
  public currentBarberShop :BarberShop = {};
  public currentPension :Pension = {};
  public currentTraining :Training = {};
  public currentTypesHaircut:TypesHaircut={};
  public currentTrainingPackage:TrainingPackage={};
  public openCloseEditAdd = 1;
  public allDograce: Array<Dograce> = [];
  public UserId :any;
  public allCitys: Array<Citys> = [];
  public allTypesHaircut: Array<TypesHaircut> = [];
  public allBarbershops: Array<BarberShop> = [];
  public allTrainingPackage: Array<TrainingPackage> = [];
  public allTraining :Array<Training> = [];
  public currentBarberShopId :BarberShop= {};
  public currentTrainingPackageId :Training= {};
  public TypesHaircutObjUpdate : TypesHaircut={};
  public TrainingPackageObjUpdate : TrainingPackage={};
  public optionDogSize = ['S','M','L'];

  @Input() mode:string | undefined;
  @Input() editUser:User = {};
  @Input() editDogOwner:Dogowner = {};
  @Input() editBarberShop:BarberShop = {};
  @Input() editPension:Pension = {};
  @Input() editTraining:Training = {};

  constructor(private DogOwnerHttp:DogOwnerService,private AuthHttp:AuthenticationService,
              private router: Router,private UserHttp:UserService,
              private DograceHttp:DogRaceService,private CitysHttp: CitysService,
              private BarberShopHttp:BarberShopService,private PensionHttp:PensionService,
              private TrainingHttp:TrainingService,private TypesHaircutHttp:TypesHaircutService,
              private TrainingPackageHttp:TrainingPackageService){}

  ngOnInit(): void {
    this.GetDogRace();
    this.GetCitys();
    this.GetTypesHaircut();
    this.GetBarberShop();
    this.GetTrainingPackage();
    this.GetTraining();
    this.CurrentBarberShopId(); // שולף את המספר של המספרה
    this.CurrentTrainingPackageId();
  }

  // משתמש
  public EditUserData = new FormGroup({
    firstName: new FormControl('',[Validators.required,Validators.pattern("[A-Za-z\u0590-\u05FF]*")]),
    lastName: new FormControl('',[Validators.required,Validators.pattern("[A-Za-z\u0590-\u05FF]*")]),
    email: new FormControl('',[ Validators.email]),
    phone: new FormControl('',[Validators.required, Validators.pattern("[0-9 ]{10}")]),
    password: new FormControl( '',[Validators.required, Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),Validators.minLength(6),Validators.maxLength(16)]),
    // ConfirmPassword: new FormControl('',[Validators.required, Validators.minLength(6),Validators.maxLength(16),Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')])
    }
  );

  // בעל כלב
  public EditDogownerData= new FormGroup({
    dogGender: new FormControl('',[Validators.required]),
    dogSize: new FormControl('',[Validators.required]),
    dogRaceId: new FormControl('',[Validators.required]),
    dogName: new FormControl('',[Validators.required]),
    castrated: new FormControl('',[Validators.required])
  });

  // מספרה
  public EditBarberShopData = new FormGroup({
    nameBarberShop: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required, Validators.pattern("[0-9 ]{10}")]),
  });

  // תספורת
  public EditTypesHaircutData= new FormGroup({
    haircutName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });

  // פנסיון
  public EditPensionData = new FormGroup({
    nameBusiness: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    pricePerForNight: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required,Validators.pattern("[0-9 ]{10}")]),
    cityId: new FormControl('',[Validators.required]),
  });

  // אילוף
  public EditTrainingData = new FormGroup({
    aaddress: new FormControl('',[Validators.required]),
    nameBusiness: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required, Validators.pattern("[0-9 ]{10}")]),
  });

  // חבילת אילוף
  public EditTrainingPackageData = new FormGroup({
    trainingName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });


  // עדכון פרטי משתמש
  async EditUser():Promise<any>{
    let userid = this.AuthHttp.GetSetUserId;
    try{
      await this.UserHttp.updateUser(Number(userid),this.EditUserData.value,this.token);
      this.finish();
    }
    catch{
    }
  };

  // עדכון פרטי בעל כלב
  async EditDogowner():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;
    this.currenDogOwner = await this.DogOwnerHttp.GetDogOwnerByUserid(Number(userid),this.token);
    try{
      this.EditDogownerData.value.userId= Number(userid);
      await this.DogOwnerHttp.updatDogOwner(Number(this.currenDogOwner.id),this.EditDogownerData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // עדכון פרטי בעל מספרה
  async EditBarberShop():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;
    this.currentBarberShop = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(userid));
    try{
      this.EditBarberShopData.value.userId= Number(userid);
      await this.BarberShopHttp.updatBarberShop(Number(this.currentBarberShop.id),this.EditBarberShopData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // עדכון פרטי בעל פנסיון
  async EditPension():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;
    this.currentPension = await this.PensionHttp.getPensionByUserId(Number(userid));
    try{
      this.EditPensionData.value.userId= Number(userid);
      await this.PensionHttp.updatPension(Number(this.currentPension.id),this.EditPensionData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // עדכון פרטי בעל מאלף כלבים
  async EditTrainin():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;
    this.currentTraining = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(userid));
    try{
      this.EditTrainingData.value.userId= Number(userid);
      await this.TrainingHttp.updatTraining(Number(this.currentTraining.id),this.EditTrainingData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // עדכון פרטי סוג תספורת
  async EditTypesHaircut():Promise<any>{
    try{
      await this.TypesHaircutHttp.updatTypesHaircut(Number(this.TypesHaircutObjUpdate.id),this.EditTypesHaircutData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // עדכון פרטי חבילת אילוף
  async EditeditTrainingPackage():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;
    this.currentTraining = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(userid));
    this.currentTrainingPackage = await this.TrainingPackageHttp.getTrainingPackageByTrainingId(Number(this.currentTraining.id));
    try{
      await this.TrainingPackageHttp.updatTrainingPackage(Number(this.TrainingPackageObjUpdate.id),this.EditTrainingPackageData.value,this.token);
      this.finish();
    }
    catch{
      alert('error');
    }
  }

  // סיום עדכון
  public finish(){
    this.router.navigate(['/user-data']).then(() => {
      window.location.reload();
    });
  }

  // שליפת גזעים
  async GetDogRace():Promise<any>{
    this.allDograce = await this.DograceHttp.GetAllDogRace();
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // שליפת מספרות
  async GetBarberShop():Promise<any>{
    this.allBarbershops = await this.BarberShopHttp.GetAllUsers_BarberShop();
  };

   // שליפת סוגי תספורות
   async GetTypesHaircut():Promise<any>{
    this.allTypesHaircut = await this.TypesHaircutHttp.GetAllTypesHaircut();
    console.log(this.allTypesHaircut);
  };

  // שליפת מאלפים
  async GetTraining():Promise<any>{
    this.allTraining = await this.TrainingHttp.GetAllUsers_Training();
  };

  // שליפת חבילות אילוף
  async GetTrainingPackage():Promise<any>{
    this.allTrainingPackage = await this.TrainingPackageHttp.GetAllTrainingPackage();
    console.log(this.allTrainingPackage);
  };

  // מחיקת תספורת
  async DeleteTypesHaircutById(id:number|undefined):Promise<any>{

    await this.TypesHaircutHttp.DeleteTypesHaircut(id,this.token);
    this.router.navigate(['/user-data']).then(() => {
      window.location.reload();
    });
  }

  // מחיקת חבילת אילוף
  async DeleteTrainingPackageById(id:number|undefined):Promise<any>{

    await this.TrainingPackageHttp.DeleteTrainingPackage(id,this.token);
    this.router.navigate(['/user-data']).then(() => {
      window.location.reload();
    });
  }

  public async CurrentBarberShopId(){
   this.currentBarberShopId = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(this.AuthHttp.GetSetUserId));
  }

  public async CurrentTrainingPackageId(){
    this.currentTrainingPackageId = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(this.AuthHttp.GetSetUserId));
  }

  // כפתור הקודם
  async onBack(){
    this.router.navigate(['/user-data']).then(() => {
      window.location.reload();
    });
  }
}
