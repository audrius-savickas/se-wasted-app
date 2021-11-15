import React, { useEffect, useState } from 'react'
import { Text, Incubator, TextArea, Picker, View, LoaderScreen } from "react-native-ui-lib"
const { TextField } = Incubator
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { FoodType } from '../../../../api/interfaces'
import { getAllTypesOfFood } from '../../../../api/type-of-food'

const textFieldCommonValues : TextFieldProps = {
    editable:false,
    centered: false
}

export const BaseInfo = () => {
    const [loading, setLoading] = useState(true)
    const [typesOfFood, setTypesOfFood] = useState<FoodType[]>([])

    useEffect(() => {
        fetchFoods()
    }, [])
    
    const fetchFoods = async () => {
        const response = await getAllTypesOfFood()
        setTypesOfFood(response)
        setLoading(false)
    }

    return (
        <View
            flex
            style={{
                flexDirection: "column"
            }}
        >
            <TextField
                label="Name *"
                validate="required"
                validationMessage="This field is required"
                textFieldCommonValues
                enableErrors
                validateOnStart
                validateOnChange
                validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
                marginT-s4
                marginB-s4
            />
            <TextField
                label="Price *"
                validate={["number", "required"]}
                validationMessage="This field is required"
                textFieldCommonValues
                enableErrors
                validateOnStart
                validateOnChange
                validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
                marginT-s4
                marginB-s4
            />
            <TextField
                label="Url of the image"
                validate="url"
                validationMessage="It must be an url"
                textFieldCommonValues
                enableErrors
                validateOnChange
                validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
                marginT-s4
                marginB-s4
            />
            <Picker 
                placeholder="Type of meal"
                showSearch
                searchPlaceholder={'Search a type of meal'}
            >
                {
                    (loading)
                    ? 
                    (
                        <LoaderScreen />
                    )
                    :
                    (
                        typesOfFood.map(option => (
                            <Picker.Item label={option.name} key={option.id} value={option.name} />
                        )) 
                    )
                }
            </Picker>
            <Text>Description</Text>
            <TextArea />
        </View>
    )
}