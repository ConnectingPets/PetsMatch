import React, { useState, useEffect, useRef } from 'react';

import { Breeds, Categories, IUserAnimals } from '../../interfaces/Interfaces';
import { AnimalBreedEnum, AnimalCategoryEnum, animalBreedEnum, animalCategoryEnum } from '../../utils/constants';
import agent from '../../api/axiosAgent';

import { CLabel } from '../common/CLabel/CLabel';

interface MarketplaceSearchProps {
    filteredPets: IUserAnimals[]
    isMarket: boolean
    onSearch: (pets: IUserAnimals[]) => void
}

interface ISearchValues {
    AnimalCategory: string
    BreedId: string
    City: string
    Gender: string
    Price: string
}

const MarketplaceSearch: React.FC<MarketplaceSearchProps> = ({ filteredPets, isMarket, onSearch }) => {
    const [categories, setCategories] = useState<Categories[]>([]);
    const [breeds, setBreeds] = useState<Breeds[]>([]);
    const [towns, setTowns] = useState<string[]>([]);
    const [searchValues, setSearchValues] = useState<ISearchValues>({
        AnimalCategory: '',
        BreedId: '',
        Gender: '',
        City: '',
        Price: ''
    });

    const animalCategoryRef = useRef<HTMLSelectElement>(null);
    const breedRef = useRef<HTMLSelectElement>(null);
    const genderRef = useRef<HTMLSelectElement>(null);
    const cityRef = useRef<HTMLSelectElement>(null);
    const priceRef = useRef<HTMLSelectElement>(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const allCategories = await agent.apiAnimal.getAllCategories();
                setCategories(allCategories.data);

                const allCities = await agent.apiUser.getAllTowns();
                setTowns(allCities.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchData();
    }, []);

    useEffect(() => {
        isMarket ? resetFilters() : resetFilters();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isMarket]);

    const resetFilters = () => {
        setSearchValues({
            AnimalCategory: '',
            BreedId: '',
            Gender: '',
            City: '',
            Price: ''
        });

        setBreeds([]);

        if (animalCategoryRef.current) {
            animalCategoryRef.current.value = '';
        }
        if (breedRef.current) {
            breedRef.current.value = '';
        }
        if (genderRef.current) {
            genderRef.current.value = '';
        }
        if (cityRef.current) {
            cityRef.current.value = '';
        }
        if (priceRef.current) {
            priceRef.current.value = '';
        }

        onSearch(filteredPets);
    };

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const onSearchChange = async (e: any) => {

        const { name, value } = e.target;
        setSearchValues(state => ({ ...state, [name]: value }));

        // if (name == 'AnimalCategory' && value != '' && breedRef.current?.value) {
        //     breedRef.current.value = '';
        //     setSearchValues(state => ({ ...state, ['BreedId']: '' }));
        // }

        if (searchValues.AnimalCategory || name == 'AnimalCategory') {
            const data = name == 'AnimalCategory' ? value : searchValues.AnimalCategory;

            if (data != '') {
                const res = await agent.apiAnimal.getAllBreeds(Number(data));

                setBreeds(res.data);

                const category = Object.keys(animalCategoryEnum).filter(x => String(animalCategoryEnum[x as keyof AnimalCategoryEnum]) == data)[0];

                filteredPets = filteredPets.filter(x => x.category == category);
            } else {
                setBreeds([]);
            }
        }

        if (searchValues.BreedId || name == 'BreedId') {
            const data = name == 'BreedId' ? value : searchValues.BreedId;

            if (data != '') {
                const breed = Object.keys(animalBreedEnum).filter(x => String(animalBreedEnum[x as keyof AnimalBreedEnum]) == data)[0];

                filteredPets = filteredPets.filter(x => x.breed == breed);
            }
        }

        if (searchValues.Gender || name == 'Gender') {
            const data = name == 'Gender' ? value : searchValues.Gender;

            if (data != '') {
                filteredPets = filteredPets.filter(x => x.gender == data);
            }
        }

        if (searchValues.City || name == 'City') {
            const data = name == 'City' ? value : searchValues.City;

            if (data != '') {
                filteredPets = filteredPets.filter(x => x.city?.toLowerCase().trim() == data?.toLowerCase().trim());
            }
        }

        if (searchValues.Price || name == 'Price') {
            const data = name == 'Price' ? value : searchValues.Price;

            if (data != '') {
                if (data.includes('-')) {
                    const range = data.split('-');
                    const firstNum = Number(range[0]);
                    const secondNum = Number(range[1]);

                    filteredPets = filteredPets.filter(x => x.price && x.price >= firstNum && x.price <= secondNum);
                } else {
                    filteredPets = filteredPets.filter(x => x.price && x.price >= 800);
                }
            }
        }

        onSearch(filteredPets);
    };

    return (
        <>
            <h3>Search by</h3>
            <form onChange={onSearchChange}>

                <CLabel inputName='AnimalCategory' title='Category' />
                <select name="AnimalCategory" id="AnimalCategory" ref={animalCategoryRef}>
                    <option>  </option>
                    {categories && categories.map(c => <option value={c.animalCategoryId} key={c.animalCategoryId}>{c.name}</option>)}
                </select>

                <CLabel inputName='BreedId' title='Breed' />
                <select name="BreedId" id="Breed" ref={breedRef}>
                    <option>  </option>
                    {categories && breeds.map(b => <option value={b.breedId} key={b.breedId}>{b.name}</option>)}
                </select>

                <CLabel inputName='Gender' title='Gender' />
                <select name="Gender" id="Gender" ref={genderRef}>
                    <option>  </option>
                    <option>Male</option>
                    <option>Female</option>
                </select>

                <CLabel inputName='City' title='City' />
                <select name="City" id="City" ref={cityRef}>
                    <option>  </option>
                    {towns && towns.map(t => <option value={t} key={t}>{t}</option>)}
                </select>

                {isMarket && (
                    <>
                        <CLabel inputName='Price' title='Price in $' />
                        <select name="Price" id="Price" ref={priceRef}>
                            <option>  </option>
                            <option>0-200</option>
                            <option>200-400</option>
                            <option>400-600</option>
                            <option>600-800</option>
                            <option>800 +</option>
                        </select>
                    </>
                )}

            </form>
        </>
    );
};

export default MarketplaceSearch;