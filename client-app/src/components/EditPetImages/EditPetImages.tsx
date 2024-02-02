import React, { useState, useEffect } from 'react';
import { FieldInputProps } from 'react-final-form';
import { DragDropContext, Draggable, DropResult, Droppable } from 'react-beautiful-dnd';
import { toast } from 'react-toastify';
import { CgAsterisk } from 'react-icons/cg';

import agent from '../../api/axiosAgent';

import { CLabel } from '../common/CLabel/CLabel';
import '../../global-styles/forms-images.scss';

interface EditPetImagesProps {
    input: FieldInputProps<File[], HTMLElement>;
    initialImages: Array<{
        [x: string]: unknown;
        id: string;
        isMain: boolean;
        url: string
    }>;
    petId: string | undefined
}

const EditPetImages: React.FC<EditPetImagesProps> = ({ input, initialImages, petId }) => {
    const [images, setImages] = useState(initialImages);

    useEffect(() => {
        setImages(initialImages);
    }, [initialImages]);

    const getPreviewUrl = (image: { id: string; isMain: boolean; url: string; file?: File }) => {
        if (image.file) {
            return URL.createObjectURL(image.file);
        } else {
            return image.url;
        }
    };

    const handleFile = async (
        e: React.ChangeEvent<HTMLInputElement>,
        input: FieldInputProps<File[], HTMLElement>,
        order: number
    ) => {
        const file = e.target.files?.[0];

        if (file && petId) {
            const formData = new FormData();
            formData.append('file', file);

            const res = await agent.apiAnimal.uploadAnimalPhoto(petId, formData);

            if (res.isSuccess) {
                toast.success(res.successMessage);

                const imageUrl = URL.createObjectURL(file);

                const updatedImages = [...images];
                updatedImages[order] = { id: res.data.id, isMain: false, url: imageUrl, file, order };
                setImages(updatedImages);

                const updatedFiles = updatedImages.map((img) => img.file);
                input.onChange(updatedFiles);
            } else {
                toast.error(res.errorMessage);
            }
        }
    };

    const handleRemoveImage = async (id: string) => {
        try {
            const res = await agent.apiAnimal.deleteAnimalPhoto(id);

            if (res.isSuccess) {
                toast.success(res.successMessage);

                const updatedImages = images.filter((img) => img.id !== id);
                setImages(updatedImages);

                const updatedFiles = updatedImages.map((img) => img.file);
                input.onChange(updatedFiles);
            } else {
                toast.error(res.errorMessage);
            }
        } catch (err) {
            console.error(err);
        }
    };

    const handleOnDragEnd = (result: DropResult) => {
        if (!result.destination) {
            return;
        }

        const updated = [...images];
        const [reorderedItem] = updated.splice(result.source.index, 1);
        updated.splice(result.destination.index, 0, reorderedItem);

        setImages(updated);
        const updatedFiles = updated.map((img) => img.file);
        input.onChange(updatedFiles);
    };

    const setMainPhoto = async (id: string) => {
        try {
            const res = await agent.apiAnimal.setAnimalMainPhoto(id, {});

            if (res.isSuccess) {
                const updatedImages = images.map((img) => ({
                    ...img,
                    isMain: img.id == id
                }));

                setImages(updatedImages);

                toast.success(res.successMessage);
            } else {
                toast.error(res.errorMessage);
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <>
            {images.length < 6 && (
                <>
                    <div className="required">
                        <CLabel inputName='Photos' title='Photo' />
                        <CgAsterisk className="asterisk" />
                    </div>
                    <div id="fileInput">
                        <input type="file" accept="image/*" multiple onChange={(e) => handleFile(e, input, images.length)} name='Photo' id='Photo' />
                        <p className="fakeFileInput" >Upload Photo</p>
                    </div>
                </>
            )}

            {images.length > 5 && (
                <p className="limit-message">Only up to 6 images can be uploaded.</p>
            )}

            {images.length > 0 && (
                <DragDropContext onDragEnd={handleOnDragEnd}>
                    <Droppable droppableId="images">
                        {(provided) => (
                            <div id="images" {...provided.droppableProps} ref={provided.innerRef}>
                                {images.map((image, index) => (
                                    <Draggable key={image.id} draggableId={image.id} index={index}>
                                        {(provided) => (
                                            <div
                                                {...provided.draggableProps}
                                                {...provided.dragHandleProps}
                                                ref={provided.innerRef}
                                                className={image.isMain ? 'image-preview main' : 'image-preview'}
                                            >
                                                <img src={getPreviewUrl(image)} alt={`preview image ${index}`} />
                                                {!image.isMain && (
                                                    <button type="button" onClick={() => handleRemoveImage(image.id)}>
                                                        X
                                                    </button>
                                                )}
                                                {image.isMain && <span className="main-message">Main photo</span>}
                                                {!image.isMain && (
                                                    <button onClick={() => setMainPhoto(image.id)} className="set-main-btn" type="button">Set as Main</button>
                                                )}
                                            </div>
                                        )}
                                    </Draggable>
                                ))}
                                {provided.placeholder}
                            </div>
                        )}
                    </Droppable>
                </DragDropContext>
            )}
        </>
    );
};

export default EditPetImages;
