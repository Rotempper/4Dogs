import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BarberShop } from 'src/app/model/barberShopModel';
import { Training } from 'src/app/model/trainingModel';
import { TrainingPackage } from 'src/app/model/TrainingPackageModel';
import { TypesHaircut } from 'src/app/model/TypesHaircutModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BarberShopService } from 'src/app/services/barber-shop.service';
import { TrainingPackageService } from 'src/app/services/training-package.service';
import { TrainingService } from 'src/app/services/training.service';
import { TypesHaircutService } from 'src/app/services/types-haircut.service';

@Component({
  selector: 'app-add-users',
  templateUrl: './add-users.component.html',
  styleUrls: ['./add-users.component.css']
})
export class AddUsersComponent implements OnInit {

  public role = this.AuthHttp.GetSetRole;
  public UserId :any;
  public error = "";
  public success = "";
  public allTypesHaircut: Array<TypesHaircut> = [];
  public allTrainingPackage: Array<TrainingPackage> = [];
  public currentBarberShop :BarberShop = {};
  public currentTypesHaircut:TypesHaircut={};
  public currentTraining :Training = {};
  public currentTrainingPackage:TrainingPackage={};

  constructor(private TypeHaircutHttp: TypesHaircutService,private router: Router,
              private AuthHttp: AuthenticationService,private BarberShopHttp: BarberShopService,
              private TrainingHttp:TrainingService,private TrainingPackageHttp:TrainingPackageService){}

  // תספורת
  public addTypesHaircutForm= new FormGroup({
    haircutName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });

  // חבילת אילוף
  public addTrainingPackageForm = new FormGroup({
    trainingName: new FormControl('',[Validators.required]),
    ddescription: new FormControl('',[Validators.required]),
    price: new FormControl('',[Validators.required]),
  });

  ngOnInit(): void {
  }

  // הוספת סוג תספורת
  async addTypesHaircut():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;// מוציא את 26 - מספר יוזר
    this.currentBarberShop = await this.BarberShopHttp.getBarberShopByUserIdObj(Number(userid)); // שומר את המשתמש הנוכחי עם 26
    this.currentTypesHaircut = await this.TypeHaircutHttp.getTypesHaircutByBarberShopId(Number(this.currentBarberShop.id)); // שומר את המס היחודי של אותו מספרה - 8

    try{
      this.addTypesHaircutForm.value.barberShopId = this.currentBarberShop.id;// מוסיף ע"פ מס יחודי של המספרה - 8
      await this.TypeHaircutHttp.AddTypesHaircut(this.addTypesHaircutForm.value);
      this.addTypesHaircutForm.reset();
      this.success = "בוצע בהצלחה."
      this.finish();
      return this.allTypesHaircut = await this.TypeHaircutHttp.GetAllTypesHaircut();
    }
    catch{
      this.error = "הוספת נתונים נכשל, אנא נסה שוב.";
      return;
    }
  };

  // הוספת חבילת אילוף
  async addTrainingPackage():Promise<any>{

    let userid = this.AuthHttp.GetSetUserId;// מוציא את 26 - מספר יוזר
    this.currentTraining = await this.TrainingHttp.getTrainingIdByUserIdObj(Number(userid));
    this.currentTrainingPackage = await this.TrainingPackageHttp.getTrainingPackageByTrainingId(Number(this.currentTraining.id));
    try{
      this.addTrainingPackageForm.value.trainingId = Number(this.currentTraining.id);
      await this.TrainingPackageHttp.AddTrainingPackage(this.addTrainingPackageForm.value);
      this.addTypesHaircutForm.reset();
      this.success = "בוצע בהצלחה."
      this.finish();
      return this.allTrainingPackage = await this.TrainingPackageHttp.GetAllTrainingPackage();
    }
    catch{
      this.error = "הוספת נתונים נכשל, אנא נסה שוב.";
      return;
    }
  };

  // סיום הוספה
  public finish(){
    this.router.navigate(['/add-users']).then(() => {
      window.location.reload();
    });
  }
}
