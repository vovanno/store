import { Component, OnInit } from '@angular/core';
import { GenreService } from 'src/app/Services/genre.service';
import { Genre } from 'src/app/Models/genre';
import { PlatformService } from 'src/app/Services/platform.service';
import { PublisherService } from 'src/app/Services/publisher.service';
import { Platform } from 'src/app/Models/platform';
import { Publisher } from 'src/app/Models/publisher';
import { Game } from 'src/app/Models/game';
import { extendedGenre } from 'src/app/Models/exetndedGenre';
import { extendedPublisher } from 'src/app/Models/extendedPublisher';
import { extendedPlatform } from 'src/app/Models/extendedPlatform';
import { FormGroup, FormControl, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { GameService } from 'src/app/Services/game.service';
import { Router } from '@angular/router';
import {debounceTime} from 'rxjs/operators';

function IsNanValidator(control: AbstractControl): {[key:string] : boolean } | null{
  if(control.value!='' && (isNaN(control.value))){
    return {'isNaN': true };
  }
  return null;
}

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.css']
})
export class CreateGameComponent implements OnInit {

  constructor(private genres: GenreService, private platforms: PlatformService, private publisher: PublisherService,
    private games: GameService, private router: Router, private fb: FormBuilder) { }

  gameModel: FormGroup;
  postGame = new Game();
  extendedGenres: extendedGenre[];
  extendedPublishers: extendedPublisher[];
  extendedPlatforms: extendedPlatform[];
  messagePool= new Object();

  hardcodedMessages= {
    required: "This field is required",
    maxlength: "Field is too long",
    minlength: "Field is too short",
    min: "This field value cannot be less than 0",
    max: "This feild value cannot be more than " + Number.MAX_VALUE,
    isNaN: "Please enter a number"
  }

  ngOnInit() {
    this.getFormData();
    this.gameModel = new FormGroup({
      gameName: new FormControl(null,[Validators.required, Validators.maxLength(50)]),
      gameDescription: new FormControl(null, [Validators.required, Validators.maxLength(500)]),
      gamePrice: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), IsNanValidator]),
      gamePublisher: new FormControl(null, [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE)]),
      gameGenre: new FormControl(null, Validators.required),
      gamePlatform: new FormControl(null, Validators.required)
    })

    for(let prop in this.gameModel.controls){
      let propName = prop.toString();
      this.messagePool[propName]='';
      this.SetWatcher(propName)
    }
  }

  SetWatcher(controlName: string){
    let control = this.gameModel.get(controlName);
    control.valueChanges.pipe(debounceTime(1000)).subscribe(() =>this.setMessage(control, controlName));
  }

  setMessage(control: AbstractControl, controlName: string){
      this.messagePool[controlName]= '';
      if((control.touched || control.dirty) && control.errors){
        this.messagePool[controlName] = Object.keys(control.errors).map(key=> 
          this.messagePool[controlName] += this.hardcodedMessages[key]).join(' ');
      }
  }
  
  isEmptyCheckboxes(){
    if((this.extendedGenres.every(p=>!p.checked)) && (this.extendedPlatforms.every(p=>!p.checked))){
      return true;
    }
    return false;
  }

  getFormData() {
    this.genres.getGenres().subscribe((data: Genre[]) => this.extendedGenres = data.map(p => { return { ...p, checked: false } }));
    this.platforms.GetPlatforms().subscribe((data: Platform[]) => this.extendedPlatforms = data.map(p => { return { ...p, checked: false } }));
    this.publisher.GetPublishers().subscribe((data: Publisher[]) => this.extendedPublishers = data.map(p => { return { ...p, checked: false } }));
  }

  CreateGame() {
    this.postGame.name = this.gameModel.controls.gameName.value;
    this.postGame.description = this.gameModel.controls.gameDescription.value;
    this.postGame.price = this.gameModel.controls.gamePrice.value;
    this.postGame.genres = this.extendedGenres.filter(p => p.checked).map(p => { return { name: p.name, genreId: p.genreId } })
    this.postGame.platforms = this.extendedPlatforms.filter(p => p.checked).map(p => { return { type: p.type, platformTypeId: p.platformTypeId } })
    this.postGame.publisherId = this.gameModel.controls.gamePublisher.value;
    this.postGame.dateOfAdding = new Date().toDateString();
    this.games.AddGame(this.postGame).subscribe((data: number) => {
      this.router.navigate(['Games']),
        this.ResetModel();
    }, (err) => console.log(err));
  }

  ResetModel() {
    this.gameModel.reset();
    this.extendedGenres.forEach(p => p.checked = false);
    this.extendedPlatforms.forEach(p => p.checked = false);
  }

}
