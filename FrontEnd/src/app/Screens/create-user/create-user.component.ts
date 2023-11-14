
import { Citys } from './../../model/citysModel';
import { TrainingService } from './../../services/training.service';
import { BarberShopService } from './../../services/barber-shop.service';
import { DogRaceService } from './../../services/dog-race.service';
import { Dograce } from './../../model/dograceModel';
import { PensionService } from './../../services/pension.service';
import { DogOwnerService } from './../../services/dog-owner.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/model/userModel';
import { Component, OnInit } from '@angular/core';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Training } from 'src/app/model/trainingModel';
import { CitysService } from 'src/app/services/citys.service';
import { ProgressBarMode } from '@angular/material/progress-bar';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { TypesHaircutService } from 'src/app/services/types-haircut.service';
import { TrainingPackageService } from 'src/app/services/training-package.service';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { FormControl, FormGroup, Validators} from '@angular/forms';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';




@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})

export class CreateUserComponent implements OnInit {
  public token = this.AutService.ShareToken;
  public allusers: Array<User> = [];
  public myUsers: Array<User> = [];
  public allDograce: Array<Dograce> = [];
  public allBarberShop: Array<BarberShop> = [];
  public myBarberShop: Array<BarberShop> = [];
  public allTypesHaircut: Array<TypesHaircut> = [];
  public allTraining: Array<Training> = [];
  public myTraining: Array<Training> = [];
  public allCitys: Array<Citys> = [];
  public allTrainingPackage: Array<TrainingPackage> = [];
  public saveRole:User = {};
  public UserId :any;
  public barberShopIdByUserId :any;
  public trainingIdByUserId :any;
  public passIsOk = false;
  public EmailOk = false;
  public radioButtonStage = 0;
  public barbarTraningStage = 0 ;   // 1 == barbara | 2 == traning
  public optionDogSize = ['S','M','L'];
  mode: ProgressBarMode = 'determinate';

  // משתמש
  public UserForm = new FormGroup({
    firstName: new FormControl('',[Validators.required,Validators.pattern("[A-Za-z\u0590-\u05FF]*")]),
    lastName: new FormControl('',[Validators.required,Validators.pattern("[A-Za-z\u0590-\u05FF]*")]),
    email: new FormControl('',[Validators.required , Validators.email]),
    phone: new FormControl('',[Validators.required, Validators.pattern("[0-9]{10}")]),
    password: new FormControl( '',[Validators.required, Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')]),
    ConfirmPassword: new FormControl('',[Validators.required,Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')])
    }
  );

  // בעל כלב
  public DogOwnerForm= new FormGroup({
    DogGender: new FormControl('',[Validators.required]),
    DogSize: new FormControl('',[Validators.required]),
    DogRaceId: new FormControl('',[Validators.required]),
    DogName: new FormControl('',[Validators.required]),
    Castrated: new FormControl('',[Validators.required])
  });

  // מספרה
  public BarberShopForm = new FormGroup({
    nameBarberShop: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required,Validators.pattern("[0-9 ]{10}")]),
  });

  // תספורת
  public TypesHaircutForm = new FormGroup({
    haircutName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });

  // פנסיון
  public PensionForm = new FormGroup({
    nameBusiness: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    pricePerForNight: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required,Validators.pattern("[0-9 ]{10}")]),
    cityId: new FormControl('',[Validators.required]),
  });

  // אילוף
  public TrainingForm = new FormGroup({
    aaddress: new FormControl('',[Validators.required]),
    nameBusiness: new FormControl('',[Validators.required]),
    cityId: new FormControl('',[Validators.required]),
    about: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required, Validators.pattern("[0-9 ]{10}")]),
  });

  // חבילת אילוף
  public TrainingPackageForm = new FormGroup({
    trainingName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });

  constructor(private UserHttp: UserService, private DogwnerHttp: DogOwnerService,
              private PensionHttp: PensionService, private DograceHttp: DogRaceService,
              private BarberShopHttp: BarberShopService, private TrainingHttp: TrainingService,
              private CitysHttp: CitysService, private TypeHaircutHttp: TypesHaircutService,
              private TrainingPackagHttp:TrainingPackageService,private AutService:AuthenticationService,
              private router: Router){}

  ngOnInit(): void {
    this.GetDogRace();
    this.GetCitys();
  };

  // סוג כניסה
  public TypeLogin(){
    if(this.mode == "determinate") //בעל כלב
    {
      this.UserForm.value.Role = "DogOwnerRole";
      sessionStorage.setItem("role", "DogOwnerRole");
      this.AutService.GetSetRole = this.saveRole.Role;
    }
    else if(this.mode == "indeterminate") // מספרה
    {
      this.UserForm.value.Role = "BarberShopRole";
      sessionStorage.setItem("role", "BarberShopRole");
      this.AutService.GetSetRole = this.saveRole.Role;
    }
    else if(this.mode == "buffer") //פנסיון
    {
      this.UserForm.value.Role = "PensionRole";
      sessionStorage.setItem("role", "PensionRole");
      this.AutService.GetSetRole = this.saveRole.Role;
    }
    else if(this.mode == "query") //מאלף
    {
      this.UserForm.value.Role = "TrainingRole";
      sessionStorage.setItem("role", "TrainingRole");
      this.AutService.GetSetRole = this.saveRole.Role;
    }
    this.radioButtonStage = 1;
  }

  // בדיקת סיסמא
  // הצגת הודעת שגיאה
  public checkpassword(confirmPassword: any){
    this.passIsOk = false; // נאפס כל פעם מחדש את המשתנה שבודק // הסיסמא תקינה
    if( confirmPassword != this.UserForm.value.password)
    {
      this.passIsOk = true; //  הסיסמא שגויה ולכן תציג הודעת שגיאה
      return;
    }
  }



  // הוספת משתמש חדש
  async creatUser():Promise<any>{

    let emailIsOk : boolean;
    emailIsOk = await this.UserHttp.GetCheckEmailInSql(this.UserForm.value.email);

    if(emailIsOk == false){ // אם האימייל לא קיים במערכת
      this.EmailOk = true; // תקין - אפשר להוסיף
      return;
    }
    else{
      this.EmailOk = false; // לא תקין
    }
    try{
      this.TypeLogin();
      await this.UserHttp.AddUser(this.UserForm.value);
      this.radioButtonStage =2;
    }
    catch{
    }
    this.myUsers = this.allusers = await this.UserHttp.GetAllUsers();
    this.UserId = await this.UserHttp.GetUseridByEmail(this.UserForm.value.email);
    return this.myUsers;
  };

  
  // הוספת בעל מספרה
  async creatBarberShop():Promise<any>{

    this.BarberShopForm.value.userId = this.UserId;
    await this.BarberShopHttp.AddBarberShop(this.BarberShopForm.value);
    this.myBarberShop = this.allBarberShop = await this.BarberShopHttp.GetAllUsers_BarberShop();
    this.barberShopIdByUserId = await this.BarberShopHttp.GetBarberShopIdByUserId(this.UserId);
    this.barbarTraningStage = 1;
    return this.myBarberShop;
  };

  // הוספת סוגי תספורות
  async creatTypesHaircut():Promise<any>{
    try{
      this.TypesHaircutForm.value.barberShopId = this.barberShopIdByUserId;
      await this.TypeHaircutHttp.AddTypesHaircut(this.TypesHaircutForm.value);
      this.TypesHaircutForm.reset();
      alert('הוסף בהצלחה');
      return this.allTypesHaircut = await this.TypeHaircutHttp.GetAllTypesHaircut();
    }
    catch{
      return;
    }
  };

  // סיום הוספה
  public finish(){
    this.router.navigate(['/Login']).then(() => {
      window.location.reload();
    });
  }

  // הוספת מאלף כלבים
  async creatTraining():Promise<any>{

    this.TrainingForm.value.userId = this.UserId;
    await this.TrainingHttp.AddTraining(this.TrainingForm.value);
    this.myTraining = this.allTraining = await this.TrainingHttp.GetAllUsers_Training();
    this.trainingIdByUserId = await this.TrainingHttp.GetTrainingIdByUserId(this.UserId);
    this.barbarTraningStage = 1;
    return this.myTraining;
  };

// הוספת חבילת אילוף
  async creatTrainingPackage():Promise<any>{

    try {
      this.TrainingPackageForm.value.trainingId = this.trainingIdByUserId;
      await this.TrainingPackagHttp.AddTrainingPackage(this.TrainingPackageForm.value);
       this.allTrainingPackage = await this.TrainingPackagHttp.GetAllTrainingPackage();
       this.TrainingPackageForm.reset();
       alert('הוסף בהצלחה ');
    }
    catch{
      return;
    }
  };

  // הוספת בעל כלב למשתמש
  async creatDogowner():Promise<any>{
    this.DogOwnerForm.value.userId=this.UserId;
    await this.DogwnerHttp.AddDogowner(this.DogOwnerForm.value);
    this.finish();
  };


  // הוספת בעל פנסיון
  async creatPension():Promise<any>{
    this.PensionForm.value.userId = this.UserId;
    await this.PensionHttp.AddPension(this.PensionForm.value);
    this.finish();
  };

  // שליפת גזעים
  async GetDogRace():Promise<any>{
    this.allDograce = await this.DograceHttp.GetAllDogRace();
  };

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

   // כפתור הקודם
   async onBack(){
    this.router.navigate([]).then(() => {
      window.location.reload();
    });
  }
}


