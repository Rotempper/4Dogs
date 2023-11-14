import { Component, Input, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Citys } from 'src/app/model/citysModel';
import { Shops } from 'src/app/model/shopsModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CitysService } from 'src/app/services/citys.service';
import { ShopsService } from 'src/app/services/shops.service';

@Component({
  selector: 'app-editShop-admin',
  templateUrl: './editShop-admin.component.html',
  styleUrls: ['./editShop-admin.component.css']
})
export class EditShopAdminComponent implements OnInit {

  public shopCustomer: Array<Shops> = [];
  public allCitys: Array<Citys> = [];
  public token = this.AuthHttp.ShareToken;
  @Input() editShop :Shops = {};

  // חנות
  public EditShopForm= new FormGroup({
    nameShop: new FormControl('',[Validators.required]),
    aaddress: new FormControl('',[Validators.required]),
    cityId: new FormControl(''),
    phone: new FormControl('',[Validators.required,Validators.pattern("[0-9 ]{10}")])
  });

  constructor(private ShopHttp:ShopsService,private CitysHttp: CitysService,
              private router: Router,private AuthHttp: AuthenticationService){}

  async ngOnInit(): Promise<void>{
    this.GetCitys();
  }

  // עדכון חנות
  async EditShopData():Promise<any>{
    if(this.EditShopForm.value.cityId == ""){
      this.EditShopForm.value.cityId = this.editShop.cityId;
    }
    try{
      await this.ShopHttp.updateShop(this.editShop.id,this.EditShopForm.value,this.token);
      this.router.navigate(['/Admin']).then(() => {
        window.location.reload();
      });
    }
    catch{
      alert('error');
    }
  }

  // שליפת ערים
  async GetCitys():Promise<any>{
    this.allCitys = await this.CitysHttp.GetAllCitys();
  };

  // שליפת נתונים - חנויות
  async GetShopData():Promise<any>{
    this.shopCustomer = await this.ShopHttp.GetAllShops(this.token);
  };

  // כפתור הקודם
  async onBack(){
    this.router.navigate([]).then(() => {
      window.location.reload();
    });
  }
}
