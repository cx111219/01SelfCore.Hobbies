import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';
import { HobbiesRoutingModule } from './hobbies-routing.module';
import { HobbiesBooksComponent } from './books/books.component';
import { HobbiesBooksInfoComponent } from './books/books-info.component';
import { HobbiesBooksEditComponent } from './books/books-edit.component';

import { HobbiesTravelsComponent } from './travels/travels.component';
import { HobbiesTravelsInfoComponent } from './travels/travels-info.component';
import { HobbiesTravelsEditComponent } from './travels/travels-edit.component';

const COMPONENTS: Type<void>[] = [
  HobbiesBooksComponent,
  HobbiesBooksInfoComponent,
  HobbiesBooksEditComponent,
  HobbiesTravelsComponent,
  HobbiesTravelsInfoComponent,
  HobbiesTravelsEditComponent
];

@NgModule({
  imports: [SharedModule, HobbiesRoutingModule],
  declarations: COMPONENTS,
  providers: [],
  entryComponents: [HobbiesTravelsEditComponent]
})
export class HobbiesModule {}
