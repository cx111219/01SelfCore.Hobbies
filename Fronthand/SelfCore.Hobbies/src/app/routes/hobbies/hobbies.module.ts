import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';
import { HobbiesRoutingModule } from './hobbies-routing.module';

const COMPONENTS: Type<void>[] = [];

@NgModule({
  imports: [SharedModule, HobbiesRoutingModule],
  declarations: COMPONENTS,
  providers: []
})
export class HobbiesModule {}
