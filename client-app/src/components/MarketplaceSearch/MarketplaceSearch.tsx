import React, { useState, useEffect } from 'react';
import { Field, Form } from 'react-final-form';

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

    const onSearchSubmit = async (values: ISearchValues) => {

        if (values.AnimalCategory) {
            const res = await agent.apiAnimal.getAllBreeds(Number(values.AnimalCategory));

            setBreeds(res.data);
        }

        if (values.AnimalCategory) {
            const category = Object.keys(animalCategoryEnum).filter(x => String(animalCategoryEnum[x as keyof AnimalCategoryEnum]) == values.AnimalCategory)[0];

            filteredPets = filteredPets.filter(x => x.category == category);
        }

        if (values.BreedId) {
            const breed = Object.keys(animalBreedEnum).filter(x => String(animalBreedEnum[x as keyof AnimalBreedEnum]) == values.BreedId)[0];

            filteredPets = filteredPets.filter(x => x.breed == breed);
        }

        if (values.Gender) {
            filteredPets = filteredPets.filter(x => x.gender == values.Gender);
        }

        if (values.City) {
            filteredPets = filteredPets.filter(x => x.city?.toLowerCase().trim() == values.City?.toLowerCase().trim());
        }

        if (values.Price) {
            if (values.Price.includes('-')) {
                const range = values.Price.split('-');
                const firstNum = Number(range[0]);
                const secondNum = Number(range[1]);

                filteredPets = filteredPets.filter(x => x.price && x.price >= firstNum && x.price <= secondNum);
            } else {
                filteredPets = filteredPets.filter(x => x.price && x.price >= 800);
            }
        }

        onSearch(filteredPets);
    };

    return (
        <>
            <h3>Search by</h3>
            <Form
                onSubmit={onSearchSubmit}
                render={({ handleSubmit }) => (
                    <form onChange={handleSubmit}>

                        <Field name='AnimalCategory'>
                            {({ input }) => (
                                <>
                                    <CLabel inputName='AnimalCategory' title='Category' />
                                    <select {...input} name="AnimalCategory" id="AnimalCategory">
                                        <option>  </option>
                                        {categories && categories.map(c => <option value={c.animalCategoryId} key={c.animalCategoryId}>{c.name}</option>)}
                                    </select>
                                </>
                            )}
                        </Field>

                        <Field name='BreedId'>
                            {({ input }) => (
                                <>
                                    <CLabel inputName='BreedId' title='Breed' />
                                    <select {...input} name="BreedId.breedId" id="Breed">
                                        <option>  </option>
                                        {categories && breeds.map(b => <option value={b.breedId} key={b.breedId}>{b.name}</option>)}
                                    </select>
                                </>
                            )}
                        </Field>

                        <Field name='Gender'>
                            {({ input }) => (
                                <>
                                    <CLabel inputName='Gender' title='Gender' />
                                    <select {...input} name="Gender" id="Gender">
                                        <option>  </option>
                                        <option>Male</option>
                                        <option>Female</option>
                                    </select>
                                </>
                            )}
                        </Field>

                        <Field name='City'>
                            {({ input }) => (
                                <>
                                    <CLabel inputName='City' title='City' />
                                    <select {...input} name="City" id="City">
                                        <option>  </option>
                                        {towns && towns.map(t => <option value={t} key={t}>{t}</option>)}
                                    </select>
                                </>
                            )}
                        </Field>

                        {isMarket && (
                            <Field name='Price'>
                                {({ input }) => (
                                    <>
                                        <CLabel inputName='Price' title='Price in $' />
                                        <select {...input} name="Price" id="Price">
                                            <option>  </option>
                                            <option>0-200</option>
                                            <option>200-400</option>
                                            <option>400-600</option>
                                            <option>600-800</option>
                                            <option>800 +</option>
                                        </select>
                                    </>
                                )}
                            </Field>
                        )}

                    </form>
                )} />
        </>
    );
};

export default MarketplaceSearch;