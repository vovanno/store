import { Component, OnInit } from '@angular/core';
import { GenreService } from 'src/app/Services/genre.service';
import { PlatformService } from 'src/app/Services/platform.service';
import { PublisherService } from 'src/app/Services/publisher.service';
import { Genre } from 'src/app/Models/genre';
import { Platform } from 'src/app/Models/platform';
import { Publisher } from 'src/app/Models/publisher';
import { FilterModel } from 'src/app/Models/filter';
import { FormControl, FormGroup,FormBuilder } from '@angular/forms';
import { GameService } from 'src/app/Services/game.service';
import { Game } from 'src/app/Models/game';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit {

  date:Date = new Date();
  dates=[
    {key:"Last week", value: new Date(new Date().setDate(this.date.getDate()-7)).toDateString()}, 
    {key:"Last month", value: new Date(new Date().setMonth(this.date.getMonth()-1)).toDateString()},
    {key:"Last year", value: new Date(new Date().setFullYear(this.date.getFullYear()-1)).toDateString()},
    {key:"Last 2 years", value: new Date(new Date().setFullYear(this.date.getFullYear()-2)).toDateString()},
    {key:"Last 3 years", value: new Date(new Date().setFullYear(this.date.getFullYear()-3)).toDateString()}
  ]
   popularity = ["Most commented", "Most viewed"];

  filterForm: FormGroup;
  filterObj = new FilterModel();


  constructor(private genres:GenreService, private platforms:PlatformService, private publishers: PublisherService, private fb: FormBuilder,
    private gameService: GameService) { }

  ngOnInit() {
    this.GetFilterData();
    this.filterForm = new FormGroup({
      dateOfPublishing: new FormControl(),
      popularity: new FormControl(),
      from: new FormControl(),
      to: new FormControl()
    });
  }

  checkedProp: boolean = false;

  GetFilterData(){
    this.genres.getGenres().subscribe((data:Genre[])=>this.genres.extendedGenre = data.map(p=>{return{... p, checked: false}}));
    this.platforms.GetPlatforms().subscribe((data:Platform[])=>this.platforms.extendedPlatform=data.map(p=>{return{... p, checked:false}}));
    this.publishers.GetPublishers().subscribe((data:Publisher[])=>this.publishers.extendedPublisher=data.map(p=>{return {... p, checked: false}}));
    
  }

  FilterData(){
    let genresData = this.genres.extendedGenre.filter(p=>p.checked===true).map(p=>{return p.genreId});
    this.filterObj.genres = genresData.length === 0 ? null : genresData;
    let platformsData = this.platforms.extendedPlatform.filter(p=>p.checked===true).map(p=>{return p.platformTypeId});
      this.filterObj.platforms = platformsData.length === 0 ? null : platformsData;
      let publishersData = this.publishers.extendedPublisher.filter(p=>p.checked===true).map(p=>{return p.publisherId});
      this.filterObj.publishers = publishersData.length === 0 ? null : publishersData;
      this.filterObj.priceFilter = {
        from: this.filterForm.controls.from.value,
        to: this.filterForm.controls.to.value
      };
      this.filterObj.dateOfAdding = new  Date(this.filterForm.controls.dateOfPublishing.value).toString();
      switch(this.filterForm.controls.popularity.value){
        case "Most commented":
          this.filterObj.isMostCommented = true;
          break;
        case "Most viewed":
          this.filterObj.isMostPopular = true;
          break;
      }
      console.log(this.filterObj);
       this.gameService.GetGamesWithFilters(this.filterObj).subscribe((data:Game[])=>this.gameService.gameData = data, (error)=> console.log(error));
      
  }
}
