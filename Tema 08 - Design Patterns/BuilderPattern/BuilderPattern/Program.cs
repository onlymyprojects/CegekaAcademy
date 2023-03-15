using BuilderPattern;

var myCat = new PetCreation(new CatPetBuilder());
var myDog = new PetCreation(new DogPetBuilder());

myCat.CreatePet();
myCat.PrintPetDetails();

myDog.CreatePet();
myDog.PrintPetDetails();