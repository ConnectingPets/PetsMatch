import React, { useState } from 'react';
import { FieldInputProps } from 'react-final-form';
import { CgAsterisk } from 'react-icons/cg';
import { DragDropContext, Draggable, DropResult, Droppable } from 'react-beautiful-dnd';

import { CLabel } from '../common/CLabel/CLabel';

import '../../global-styles/forms-images.scss';

interface PetImagesProps {
    input: FieldInputProps<File[], HTMLElement>
}

const PetImages: React.FC<PetImagesProps> = ({ input }) => {
    const [images, setImages] = useState<string[]>([]);
    const [mainPhotoIndex, setMainPhotoIndex] = useState<number>(0);

    const handleFile = (
        e: React.ChangeEvent<HTMLInputElement>,
        input: FieldInputProps<File[], HTMLElement>
    ) => {
        const file = e.target.files?.[0];

        if (file) {
            const imageUrl = URL.createObjectURL(file);

            setImages([...images, imageUrl]);
            input.onChange([...input.value, file]);
        }
    };

    const handleRemoveImage = (index: number, input: FieldInputProps<File[], HTMLElement>) => {
        const updateImages = [...images];
        updateImages.splice(index, 1);
        setImages(updateImages);

        const updateFiles = [...input.value];
        updateFiles.splice(index, 1);
        input.onChange(updateFiles);

        if (index == mainPhotoIndex) {
            setMainPhotoIndex(0);
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
    };

    const setMainPhoto = (index: number) => {
        const updatedImages = [...images];
        const mainPhoto = updatedImages.splice(index, 1);
        updatedImages.unshift(mainPhoto[0]);
        setImages(updatedImages);

        const updatedFiles = [...input.value];
        const mainPhotoFile = updatedFiles.splice(index, 1);
        input.onChange([...mainPhotoFile, ...updatedFiles]);
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
                        <input type="file" accept="image/*" multiple onChange={(e) => handleFile(e, input)} name='Photo' id='Photo' />
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
                            <div id="images" {...provided.droppableProps} ref={provided.innerRef} >
                                {images.map((imageUrl, index) => (
                                    <Draggable key={index} draggableId={`image-${index}`} index={index} >
                                        {(provided) => (
                                            <div {...provided.draggableProps} {...provided.dragHandleProps} ref={provided.innerRef} className={index == 0 ? 'image-preview main' : 'image-preview'}>
                                                <img src={imageUrl} alt="preview image" />
                                                <button type="button" onClick={() => handleRemoveImage(index, input)}>X</button>
                                                {index == mainPhotoIndex && <span className="main-message">Main photo</span>}
                                                {index != mainPhotoIndex && (
                                                    <button onClick={() => setMainPhoto(index)} className="set-main-btn" type="button">Set as Main</button>
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

export default PetImages;