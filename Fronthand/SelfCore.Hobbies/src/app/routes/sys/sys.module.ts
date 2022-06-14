import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';
import { SysRoutingModule } from './sys-routing.module';
import { SysUserComponent } from './user/user.component';
import { SysUserinfoComponent } from './user/userinfo.component';

const COMPONENTS: Type<void>[] = [SysUserComponent, SysUserinfoComponent];

@NgModule({
  imports: [SharedModule, SysRoutingModule],
  declarations: COMPONENTS
})
export class SysModule {}
