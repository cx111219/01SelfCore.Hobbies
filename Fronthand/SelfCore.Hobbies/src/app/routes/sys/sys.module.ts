import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';
import { SysRoutingModule } from './sys-routing.module';
import { SysUserComponent } from './user/user.component';
import { SysUserEditComponent } from './user/userEdit.component';
import { SysUserInfoComponent } from './user/userInfo.component';

const COMPONENTS: Type<void>[] = [SysUserComponent, SysUserInfoComponent, SysUserEditComponent];

@NgModule({
  imports: [SharedModule, SysRoutingModule],
  declarations: COMPONENTS
})
export class SysModule {}
