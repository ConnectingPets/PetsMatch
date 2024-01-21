import React, { useEffect, useState } from 'react';
import { Field, Form } from 'react-final-form';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import { Breeds, Categories, IUserAnimals } from '../../interfaces/Interfaces';
import { AnimalBreedEnum, AnimalCategoryEnum, animalBreedEnum, animalCategoryEnum } from '../../utils/constants';
import agent from '../../api/axiosAgent';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import { CLabel } from '../../components/common/CLabel/CLabel';
import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import Footer from '../../components/Footer/Footer';
import './AdoptionMarketCatalogPage.scss';

interface AdoptionMarketCatalogPageProps { }

interface ISearchValues {
    AnimalCategory: string
    BreedId: string
    City: string
    Gender: string
    Price: string
}

const AdoptionMarketCatalogPage: React.FC<AdoptionMarketCatalogPageProps> = observer(() => {
    const [isMarket, setIsMarket] = useState<boolean>(true);
    const [allPetsInMarket, setAllPetsInMarket] = useState<IUserAnimals[]>([]);
    const [petsInMarket, setPetsInMarket] = useState<IUserAnimals[]>([]);
    const [allPetsForAdoption, setAllPetsForAdoption] = useState<IUserAnimals[]>([]);
    const [petsForAdoption, setPetsForAdoption] = useState<IUserAnimals[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [itemsPerPage] = useState<number>(12);
    const [categories, setCategories] = useState<Categories[]>([]);
    const [breeds, setBreeds] = useState<Breeds[]>([]);
    const [towns, setTowns] = useState<string[]>([]);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = isMarket
        ? petsInMarket.slice(indexOfFirstItem, indexOfLastItem)
        : petsForAdoption.slice(indexOfFirstItem, indexOfLastItem);

    const pages = Math.ceil((isMarket ? petsInMarket.length : petsForAdoption.length) / itemsPerPage);

    const paginationButtons = () => {
        const pageButtons = [];

        for (let i = 1; i <= pages; i++) {
            pageButtons.push(
                <button key={i} onClick={() => setCurrentPage(i)} className={i == currentPage ? 'current-btn' : ''}>
                    {i}
                </button>
            );
        }

        return pageButtons;
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const allPetsInMarketResult = await agent.apiMarketplace.getAllAnimalsInMarketplace();
                setPetsInMarket(allPetsInMarketResult.data);
                setAllPetsInMarket(allPetsInMarketResult.data);

                const allPetsForAdoptionResult = await agent.apiAdoption.getAllAnimalsForAdoption();
                setPetsForAdoption(allPetsForAdoptionResult.data);
                setAllPetsForAdoption(allPetsForAdoptionResult.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchData();
    }, []);

    const onClickAdoptionMarket = () => {
        setIsMarket(state => !state);

        setCurrentPage(1);

        // onSearchSubmit({
        //     AnimalCategory: '',
        //     BreedId: '',
        //     City: '',
        //     Gender: ''
        // });
    };

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

        let filteredPets = isMarket ? allPetsInMarket : allPetsForAdoption;

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
            filteredPets = filteredPets.filter(x => x.city == values.City);
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

        isMarket ? setPetsInMarket(filteredPets) : setPetsForAdoption(filteredPets);
    };

    return (
        <div className={themeStore.isLightTheme ? 'catalog-wrapper' : 'catalog-wrapper catalog-wrapper__dark'}>
            <FormsHeader title='Pets Catalog' />

            <article className={themeStore.isLightTheme ? 'catalog-wrapper__article ' : 'catalog-wrapper__article catalog-wrapper__article__dark '}>
                <section className='catalog-wrapper__article__options'>
                    <div>
                        <h3 className={isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>Marketplace</h3>
                    </div>
                    <div>
                        <h3 className={!isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>For Adoption</h3>
                    </div>
                </section>

                <section className='catalog-wrapper__article__search'>
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
                </section>

                <section className='catalog-wrapper__pets'>
                    {!isMarket && currentItems.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='catalogAdoption' key={x.id} />)}

                    {isMarket && currentItems.map(x => <CPetCard name={x.name} photo={x.mainPhoto} price={x.price} id={x.id} buttons='catalogMarket' key={x.id} />)}
                </section>

                {currentItems.length == 0 && (
                    <div className='catalog-wrapper__empty-page'>
                        <p>No pets found matching these filters!</p>
                    </div>
                )}

                <div className='catalog-wrapper__pagination-btn'>{paginationButtons()}</div>
            </article>

            <Footer />
        </div>
    );
});

export default AdoptionMarketCatalogPage;