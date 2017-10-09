//
// confirmations.es
//
// confirmations for es-US
//


var monthTTS = new Array("empty",
			 "enero", 
			 "febrero", 
			 "marzo", 
			 "abril", 
			 "mayo",
			 "junio", 
			 "julio", 
			 "agosto", 
			 "septiembre", 
			 "octubre", 
			 "noviembre", 
			 "diciembre");


var day_of_monthTTS = new Array("empty",
			   "el primero",
			   "el dos",
			   "el tres",
			   "el cuatro",
			   "el cinco",
			   "el seis",
			   "el siete",
			   "el ocho",
			   "el nueve",
			   "el diez",
			   "el once",
			   "el doce",
			   "el trece",
			   "el catorce",
			   "el quince",
			   "el dieciseis",
			   "el diecisiete",
			   "el dieciocho",
			   "el diecinueve",
			   "el veinte",
			   "el veintiuno",
			   "el veintidós",
			   "el veintitrés",
			   "el veinticuatro",
			   "el veinticinco",
			   "el veintiséis",
			   "el veintisiete",
			   "el veintiocho",
			   "el veintinueve",
			   "el treinta",
			   "el treinta y uno");

var cardinalTTS = new Array("cero",
			    "uno",
			    "dos",
			    "tres",
			    "cuatro",
			    "cinco",
			    "seis",
			    "siete",
			    "ocho",
			    "nueve",   
			    "diez",
			    "once",
			    "doce",
			    "trece",
			    "catorce",
			    "quince",
			    "dieciséis",
			    "diecisiete",
			    "dieciocho",
			    "diecinueve",
			    "veinte",
			    "veintiuno",
			    "veintidós",
			    "veintitrés",
			    "veinticuatro",
			    "veinticinco",
			    "veintiséis",
			    "veintisiete",
			    "veintiocho",
			    "veintinueve",
			    "treinta",
			    "treinta y uno",
			    "treinta y dos",
			    "treinta y tres",
			    "treinta y cuatro",
			    "treinta y cinco",
			    "treinta y seis",
			    "treinta y siete",
			    "treinta y ocho",
			    "treinta y nueve",
			    "cuarenta",
			    "cuarenta y uno",
			    "cuarenta y dos",
			    "cuarenta y tres",
			    "cuarenta y cuatro",
			    "cuarenta y cinco",
			    "cuarenta y seis",
			    "cuarenta y siete",
			    "cuarenta y ocho",
			    "cuarenta y nueve",
			    "cincuenta",
			    "cincuenta y uno",
			    "cincuenta y dos",
			    "cincuenta y tres",
			    "cincuenta y cuatro",
			    "cincuenta y cinco",
			    "cincuenta y seis",
			    "cincuenta y siete",
			    "cincuenta y ocho",
			    "cincuenta y nueve",
			    "sesenta",
			    "sesenta y uno",
			    "sesenta y dos",
			    "sesenta y tres",
			    "sesenta y cuatro",
			    "sesenta y cinco",
			    "sesenta y seis",
			    "sesenta y siete",
			    "sesenta y ocho",
			    "sesenta y nueve",
			    "setenta",
			    "setenta y uno",
			    "setenta y dos",
			    "setenta y tres",
			    "setenta y cuatro",
			    "setenta y cinco",
			    "setenta y seis",
			    "setenta y siete",
			    "setenta y ocho",
			    "setenta y nueve",
			    "ochenta",
			    "ochenta y uno",
			    "ochenta y dos",
			    "ochenta y tres",
			    "ochenta y cuatro",
			    "ochenta y cinco",
			    "ochenta y seis",
			    "ochenta y siete",
			    "ochenta y ocho",
			    "ochenta y nueve",
			    "noventa",
			    "noventa y uno",
			    "noventa y dos",
			    "noventa y tres",
			    "noventa y cuatro",
			    "noventa y cinco",
			    "noventa y seis",
			    "noventa y siete",
			    "noventa y ocho",
			    "noventa y nueve");


var hundredsTTS = new Array("empty",
			    "ciento",
			    "doscientos",
			    "trescientos",
			    "cuatrocientos",
			    "quinientos",
			    "seiscientos",
			    "setecientos",
			    "ochocientos",
			    "novecientos");
			 
/*******************************************
 * confirmCommand
 *******************************************/
function confirmCommand (args)   { 

    // Confirmation arguments
    var directory = args.baselinepromptsdirectory+"command/";
    var confValue = args.dm_confirm_string;

    // Create prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    // Add prompts
    prompts.audio.push(directory + confValue + ".ulaw");
    prompts.tts.push(confValue);

    return prompts;
}

/*******************************************
 * confirmAlphanum
 *******************************************/
function confirmAlphanum (args)   {

    // Confirmation arguments
    var directory = args.baselinepromptsdirectory+"chars/";
    var confValue = args.value;

    // Create prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    // Break into an array of characters
    var characters = confValue.match(/\w/g);
  
    // Add prompts to the array
    for (var c=0; c<characters.length; c++) {
	prompts.audio.push( directory + characters[c].charCodeAt(0) + ".ulaw" );
	prompts.tts.push( characters[c] );
    }

    return prompts;
}

/*******************************************
 * confirmCCExpDate
 *******************************************/
function confirmCCExpDate(args){
    
    //Confirmation arguments
    var directory = args.baselinepromptsdirectory;
    var confValue = args.value;

    var prompts = new Object();

    //Initialize the prompts array
    prompts.audio = new Array();
    prompts.tts   = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    var century = confValue.substr( 0 , 2 );
    var month   = confValue.substr( 4 , 2 );
    var year2   = confValue.substr( 2 , 2 );
    var year4   = confValue.substr( 0 , 4 );

    // End-of prompts
    prompts.audio.push( directory + "ccexpdate/end_of.ulaw" );
    prompts.tts.push( "fin de" );
 
    // Month
    prompts.audio.push(directory + "date/" + month + "m.ulaw");
    prompts.tts.push(monthTTS[Math.round(month)]);

    prompts.audio.push(directory + "date/de.ulaw");
    prompts.tts.push("de");

    // Year - es-US always has year in natural numbers
    var yearPrompts = new Array();
    yearPrompts = naturalNumber( year4, 0 );

    for (var i=0 ; i < yearPrompts.audio.length ; i++ ) 
	{
	    prompts.audio.push( directory+yearPrompts.audio[i] );
	    prompts.tts.push( yearPrompts.tts[i] );
	}
    
    return prompts;
}

/*******************************************
 * confirmCreditCard
 *******************************************/
function confirmCreditCard( args )
{
    //Confirmation arguments
    var directory = args.baselinepromptsdirectory;
    var confValue = args.value;
    var cardtype = args.value.CARDTYPE;

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    var prompts = new Object();
    var numbers = new Array();

    //Initialize the prompts array
    prompts.audio = new Array();
    prompts.tts   = new Array();
    
        
    if ( cardtype!=null && cardtype.length != 0 && cardtype != "private" )
	{
	    prompts.audio.push( directory + "creditcard/" + cardtype + ".ulaw");
	    prompts.tts.push( cardtype );

	    prompts.audio.push( directory + "silence500ms.ulaw");
	    prompts.tts.push( " " );
	}

    if( confValue != null && confValue != "")
	{
	    if (cardtype == "amex" )
		{
		    // grouped as 4/6/5 (tot 15)
		    numbers.push( confValue.substr(0,4) );
		    numbers.push( confValue.substr(4,6) );
		    numbers.push( confValue.substr(10,5) );
		}
	    else if ( cardtype == "mastercard" || cardtype == "visa" || cardtype == "discover" )
		{
		    if ( confValue.length==16 )
			{
			    // mastercard, visa and discover, grouped as 4/4/4/4 (tot 16)
			    numbers.push( confValue.substr(0,4) );
			    numbers.push( confValue.substr(4,4) );
			    numbers.push( confValue.substr(8,4) );
			    numbers.push( confValue.substr(12,4) );
			}
		    else
			{
			    // mastercard and visa, grouped as 4/3/3/3 (tot 13)
			    numbers.push( confValue.substr(0,4) );
			    numbers.push( confValue.substr(4,3) );
			    numbers.push( confValue.substr(7,3) );
			    numbers.push( confValue.substr(10,3) ); 
			}
		}
	    else if (cardtype == "dinersclub" )
		{
		    // grouped as 4/6/4 (tot 14)
		    numbers.push( confValue.substr(0,4) );
		    numbers.push( confValue.substr(4,6) );
		    numbers.push( confValue.substr(10,4) );
		}

	    /* PlayAudioForCreditcard doen't know any other credit card
	       number format */
	    if ( cardtype != "amex" && cardtype != "dinersclub" &&
		 cardtype != "discover" && cardtype != "visa" &&
		 cardtype != "mastercard")
		{
		    numbers.push( confValue );
		}

	    /* Playing credit card numbers with silence prompts in between grouped numbers */
	    for( var i = 0 ; i < numbers.length ; i++ )
		{
		    var toalphanum = new Object();
		    toalphanum.value = numbers[i];
		    toalphanum.baselinepromptsdirectory = directory;
		    var spellprompt = confirmAlphanum( toalphanum );
             
		    for( var j = 0 ; j < spellprompt.audio.length ; j++ )
			{
			    prompts.audio.push( spellprompt.audio[j] );
			    prompts.tts.push( spellprompt.tts[j] );
			}
                
		    if( i < ( numbers.length - 1 ) )
			{
			    prompts.audio.push( directory + "silence500ms.ulaw");
			    prompts.tts.push( " " );
			}
		}
	}
    
    return prompts;
} 

/*******************************************
 * confirmCurrency
 *******************************************/
function confirmCurrency(args)
{
    //Confirmation arguments
    var directory = args.baselinepromptsdirectory;
    var confValue = args.value;
    var cardtype = args.value.CARDTYPE;

    var prompts = new Object();
    
    //Initialize the prompts array
    prompts.audio = new Array();
    prompts.tts   = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    var qualifier = confValue.substr(0, 1);
    var dollar_pesos = "USD";

    if( qualifier == "U" || qualifier == "P" ) {
	dollar_pesos = confValue.substr(0, 3);
	confValue = confValue.substr(3, confValue.length);
    }

    var dollars;
    var cents;
    
    var currArray = confValue.split( '.' )

	if ( currArray.length == 2 )
	    {
		dollars = currArray[0];
		cents   = currArray[1];
	    }

    // Handle invalid amounts by simply using it as the filename
    if (dollars == null || cents == null)
	{
	    prompts.audio.push( directory + confValue );
	    prompts.tts.push( confValue );
	    return prompts;
	}
      
    var spellprompt = naturalNumber( dollars, 1 );

    // Don't play the '0 dollars' if the amount is only cents.
    if( parseInt(dollars,10) > 0 ) {
    	for( var j = 0 ; j < spellprompt.audio.length ; j++ )
	    {
		prompts.audio.push( directory + spellprompt.audio[j] );
        	prompts.tts.push( spellprompt.tts[j] );
	    }
	
    	if( ( parseInt(dollars,10) > 1 ) || ( parseInt( dollars,10) == 0 ) )
	    {
		// Play "dollars"
		prompts.audio.push( directory + "currency/dollars.ulaw" );
		prompts.tts.push( "dólares" );
	    } else {
		// Play "dollar"
		prompts.audio.push( directory + "currency/dollar.ulaw" );
		prompts.tts.push( "dólar" );
	    }
    }
      
    // Only play the cents part if different from 0 cent
    if ( parseInt(cents,10) > 0)
	{
	    // Don't play the 'and' if there is no dollar amount
	    if( parseInt(dollars,10) > 0 ) {
	        // Play "and"
		prompts.audio.push( directory + "currency/and.ulaw" );
        	prompts.tts.push( "con" );
	    }
	    
	    spellprompt = new Array();
	    spellprompt = naturalNumber( cents, 1 );
	    
	    for( var k = 0 ; k < spellprompt.audio.length ; k++ )
		{
		    prompts.audio.push( directory + spellprompt.audio[k] );
		    prompts.tts.push( spellprompt.tts[k] );
		}
	    
	    if( parseInt( cents , 10 ) > 1 )
		{
		    // Play "cents"
		    prompts.audio.push( directory + "currency/cents.ulaw" );
		    prompts.tts.push( "centavos" );
		}
	    else
		{
		    // Play "cents"
		    prompts.audio.push( directory + "currency/cent.ulaw" );
		    prompts.tts.push( "centavo" );
		}
	}
    
    return prompts;
}

/*******************************************
 * confirmCustomContext
 *******************************************/
function confirmCustomContext (args)   {

    // Confirmation arguments
    var directory = args.baselinepromptsdirectory;
    var confValue = args.dm_confirm_string;

    // Result object
    var prompts = new Object();

    // Initialize the prompts array 
    prompts.audio = new Array();
    prompts.tts = new Array();

    // Set confirmation prompt and tts values in the return object's arrays
    prompts.audio.push(directory + confValue + ".ulaw");
    prompts.tts.push(confValue);

    return prompts;
}

/*******************************************
 * confirmDate
 *******************************************/
function confirmDate (args)   {

    // Prompts Directory
    var directory = args.baselinepromptsdirectory;
    // Choose meaning for a builtin
    var confValue = args.value;

    // Result object
    var prompts = new Object();

    // Initialize the prompts array 
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    // Parse the value string YYYYMMDD
    var year4 = confValue.substr(0,4);
    var century = confValue.substr(0,2);
    var year2 = confValue.substr(2,2);
    var month = confValue.substr(4,2);
    var day = confValue.substr(6,2);


    // Day    
    prompts.audio.push(directory + "date/"+day+"d.ulaw");
    prompts.tts.push(day_of_monthTTS[Math.round(day)]);

    prompts.audio.push(directory + "date/de.ulaw");
    prompts.tts.push("de");

    // Month
    prompts.audio.push(directory + "date/" + month + "m.ulaw");
    prompts.tts.push(monthTTS[Math.round(month)]);
    
    prompts.audio.push(directory + "date/de.ulaw");
    prompts.tts.push("de");

    // Year
    var yearPrompts = naturalNumber(year4, 0);

    // Don't say 200-zero.  So if the last number is a zero, then
    // don't say it.
    if( yearPrompts.tts[ yearPrompts.tts.length - 1 ] == '0' ) {
	for (var i=0;i<yearPrompts.audio.length-1;i++) {
    	    prompts.audio.push(directory+yearPrompts.audio[i]);
	    prompts.tts.push(yearPrompts.tts[i]);
	}
    } else {
	for (var i=0;i<yearPrompts.audio.length;i++) {
    	    prompts.audio.push(directory+yearPrompts.audio[i]);
	    prompts.tts.push(yearPrompts.tts[i]);
	}
    }
    
    return prompts;
}

/*******************************************
 * confirmDigits
 *******************************************/
function confirmDigits (args)   {

    var directory = args.baselinepromptsdirectory+"cardinal/";
    var confValue = args.value;

    // Result object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    // Break into an array of digits
    var digits = confValue.match(/\d/g);
  
    for (var d=0; d<digits.length; d++) {
	prompts.audio.push(directory + digits[d] + ".ulaw");
	prompts.tts.push(digits[d]);
    }

    return prompts;
}

/*******************************************
 * confirmNaturalNumbers
 *******************************************/
function confirmNaturalNumbers( args )
{
    var directory = args.baselinepromptsdirectory
	var confValue = args.value;

    //Result object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    if ( confValue.substr(0,1) == "-" )
	{
	    result.append("<audio src=\"" + promptURL + "/naturalnumbers/negative.ulaw\">negative</audio>\n");
	    confValue = confValue.substr(1);
	}
      
    var integer;
    var decimal;

    if ( confValue.indexOf(".") != -1 )
	{
	    var currArray = confValue.split( '.' )

		if ( currArray.length == 2 )
		    {
			integer = currArray[0];
			decimal = currArray[1];
		    }
		else
		    {
			integer = 0;
			decimal = currArray[0];
		    }
	}
    else
	{
	    integer = confValue;
	    decimal = "0";
	}
   
    var spellprompt = naturalNumber(integer, 0);

    // Don't say things like 100-zero, so remove the last zero if it exists.           
    if( spellprompt.tts[ spellprompt.tts.length - 1 ] == '0' ) {
	for( var k = 0 ; k < spellprompt.audio.length-1; k++ )
	    {
        	prompts.audio.push( directory + spellprompt.audio[k] );
	        prompts.tts.push( spellprompt.tts[k] );
	    }
    } else {
	for( var k = 0 ; k < spellprompt.audio.length ; k++ )
	    {
        	prompts.audio.push( directory + spellprompt.audio[k] );
	        prompts.tts.push( spellprompt.tts[k] );
	    }
    }
    
    // Only play the decimal part if different from 0
    if ( parseInt( decimal , 10 ) > 0 )
	{
	    // Play "point"
	    prompts.audio.push( directory + "naturalnumbers/point.ulaw");
	    prompts.tts.push( "punto" );

	    var toalphanum = new Object();
	    toalphanum.baselinepromptsdirectory = directory;
	    toalphanum.value = decimal;

	    var alphanumprompt = confirmAlphanum( toalphanum );
           
	    for( var k = 0 ; k < alphanumprompt.audio.length ; k++ )
		{
		    prompts.audio.push( alphanumprompt.audio[k] );
		    prompts.tts.push( alphanumprompt.tts[k] );
		}
	}

    return prompts;
}

/*******************************************
 * confirmPhone
 *******************************************/
function confirmPhone (args)  {

    var directory = args.baselinepromptsdirectory+"cardinal/";
    // Choose meaning for a builtin
    var confValue = args.value;

    // Result object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    var phonenumber = confValue;
    var extension = "";
    var extIndex = confValue.indexOf("x");

    // Splitting phone number and the extension for confirmation
    if( extIndex != -1 )
	{
	    phonenumber = confValue.substring( 0 , extIndex );
	    extension   = confValue.substring( extIndex+1 );
	}

    if( phonenumber.length == 11 && phonenumber.charAt(0) == "1" )
	{
	    prompts.audio.push( directory + "1.ulaw" );
	    prompts.tts.push( "uno" );

	    phonenumber = phonenumber.substring( 1 );
	}

    // Confirming the leading "1" as the country code and 800 as "8 hundred"
    if ( phonenumber.length == 10 ) 
	{
	    /*if( args.raw.substring( 0 , 1 ) == "1" || args.raw.toUpperCase().substring( 0 , 3 ) == "ONE" )
		{
		    prompts.audio.push( directory + "1.ulaw" );
		    prompts.tts.push( "uno" );
		}
	    */
	    if( phonenumber.substring( 0 , 1 ) == "1") {
	    
		prompts.audio.push( directory + "1.ulaw" );
		prompts.tts.push( "uno" );
	    }
	
	    if( phonenumber.substring(0 , 3 ) == "800") 
		{
		    var spellprompt = naturalNumber("800", 0);
             
		    for( var j = 0 ; j < spellprompt.audio.length ; j++ )
			{
			    prompts.audio.push( args.baselinepromptsdirectory + spellprompt.audio[j] );
			    prompts.tts.push( spellprompt.tts[j] );
			}
			
		    phonenumber = phonenumber.substring( 3 );
		}
	} 

    // Break into an array of digits
    var digits = phonenumber.match(/\d/g);
  
    for (var d=0; d<digits.length; d++) {
	prompts.audio.push(directory + digits[d] + ".ulaw");
	prompts.tts.push(digits[d]);
	if ((digits.length==10) && ((d==2)||(d==5))) {
	    // Include pause
	    prompts.audio.push(directory + "silence500ms.ulaw");
	    prompts.tts.push(",");
	}
    }

    // Confirming the extension
    if( extension != null && extension.length > 0 )
	{
	    prompts.audio.push(directory + "/extension.ulaw");
	    prompts.tts.push("extension");

	    args.value = extension;
	    var spellprompt = confirmAlphanum( args );
             
	    for( var j = 0 ; j < spellprompt.audio.length ; j++ )
		{
		    prompts.audio.push( spellprompt.audio[j] );
		    prompts.tts.push( spellprompt.tts[j] );
		}
	}

    return prompts;
}

/*******************************************
 * confirmSocialSecurity
 *******************************************/
function confirmSocialSecurity( args )
{
    // Confirmation arguments
    var directory = args.baselinepromptsdirectory;
    var confValue = args.value;

    // Create prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	} 

    var numbers = new Array();
    numbers.push( confValue.substr( 0 , 3 ) );
    numbers.push( confValue.substr( 3 , 2 ) );
    numbers.push( confValue.substr( 5 , 4 ) );
    
    for( var i = 0 ; i < numbers.length ; i++ )
	{
	    var toalphanum = new Object();
	    toalphanum.value = numbers[i];
	    toalphanum.baselinepromptsdirectory = directory;
	    var spellprompt = confirmAlphanum( toalphanum );
             
	    for( var j = 0 ; j < spellprompt.audio.length ; j++ )
		{
		    prompts.audio.push( spellprompt.audio[j] );
		    prompts.tts.push( spellprompt.tts[j] );
		}
                
	    if( i < ( numbers.length - 1 ) )
		{
		    prompts.audio.push( directory + "silence500ms.ulaw");
		    prompts.tts.push( " " );
		}
	}

    return prompts;
}

/*******************************************
 * confirmTime
 *******************************************/
function confirmTime (args)  {

    var directory = args.baselinepromptsdirectory;
    // Choose meaning for a builtin
    var confValue = args.MEANING;

    // Prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    if( confValue.length != 5 )
	{
	    prompts.audio.push(directory + confValue + ".ulaw");
	    prompts.tts.push(confValue);
	    return prompts;
	}
    // Parse the value string HHMM
    var hour      = Math.round( confValue.substr( 0 , 2 ) );
    var minute    = confValue.substr( 2 , 2 );
    var time      = confValue.substr( 0 , 4 ); 
    var fifthChar = confValue.substr( 4 , 5 );
    var qualifier = args.value.QUALIFIER;

    // validate number accoridng to format
    if( fifthChar == "h" || fifthChar == "?" )	{
	if( time < 0 || time > 2400) {
	    prompts.audio.push(directory + confValue + ".ulaw");
	    prompts.tts.push(confValue);
	    return prompts;
	}

	// All es-US playback in 12 hour clock --> OSDM 1.x assumption
	if(hour > 12) {
	    hour -= 12;
	    fifthChar = "p";
	}
	else {
	    fifthChar = "a";
	}
    }        
    else if ( fifthChar == "a" || fifthChar == "p" ) {
	if ( time < 0 || time > 1259 ) {
	    prompts.audio.push(directory + confValue + ".ulaw");
	    prompts.tts.push(confValue);
	    return prompts;
	}
    }
    
    //Playing qualifier if necessary : approximately, before, after
    /*
      if( qualifier != null )
          {
	    if( qualifier == "approx" )
		{
		    prompts.audio.push(directory + "time/approximately.ulaw");
		    prompts.tts.push( "approximately" );
		}
	    else if ( qualifier == "before" )
		{
		    prompts.audio.push(directory + "time/before.ulaw");
		    prompts.tts.push( "before" );
		}
	    else if ( qualifier == "after")
		{
		    prompts.audio.push(directory + "time/after.ulaw");
		    prompts.tts.push( "after" );
		}
	}
    */

    //Playing the hour
    prompts.audio.push(directory + "time/"+hour+"H.ulaw");
    if(hour == 1) {
	prompts.tts.push("la una");
    }
    else {
	prompts.tts.push("las " + cardinalTTS[Math.round(hour)]);
    }

    //Playing minutes if needed
    if( minute != "00" ) {

	prompts.audio.push( directory + "time/conn.ulaw" );
	prompts.tts.push("con");

	if(minute == "01") {
	    prompts.audio.push( directory + "cardinal/un.ulaw" );
	    prompts.tts.push("un");
	    prompts.audio.push( directory + "time/minuto.ulaw" );
	    prompts.tts.push("minuto");
	}
	else {
	    prompts.audio.push( directory + "cardinal/" + minute + ".ulaw" );
	    prompts.tts.push( cardinalTTS[Math.round(minute)] );
	    prompts.audio.push( directory + "time/minutos.ulaw" );
	    prompts.tts.push("minutos");
	}
    }
    else {
	prompts.audio.push( directory + "time/en_punto.ulaw" );
	prompts.tts.push("en punto");
    }

    //Playing am, pm or hours if needed

    /*
      0100a-1159a --> de la mañana
      1200p-0659p --> de la tarde
      0700p-1259a --> de la noche
    */

    if( fifthChar == "a" ) {
	if( hour < 12 ) {
	    prompts.audio.push( directory + "time/de_la_manana.ulaw" );
	    prompts.tts.push( "de la mañana" );
	}
	else {
	    // 1200a - 1259a
	    prompts.audio.push( directory + "time/de_la_noche.ulaw" );
	    prompts.tts.push( "de la noche" );
	}
    }
    else {
	if ( hour > 7 && hour < 12 ) {
	    prompts.audio.push( directory + "time/de_la_noche.ulaw" );
	    prompts.tts.push( "de la noche" );
	}
	else {
	    prompts.audio.push( directory + "time/de_la_tarde.ulaw" );
	    prompts.tts.push( "de la tarde" );
	}
    }
    
    return prompts;
}

/*******************************************
 * confirmZipCode
 *******************************************/
function confirmZipCode( args )
{
    var directory = args.baselinepromptsdirectory;
    // Choose meaning for a builtin
    var confValue = args.MEANING;

    // Prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}

    var numbers = new Array();
    numbers.push( confValue.substr( 0 , 5 ) );

    if( confValue.length == 9 ) {
        numbers.push( confValue.substr( 5 , 4 ) );
    }

    for( var i = 0 ; i < numbers.length ; i++ )
	{
	    var toalphanum = new Object();
	    toalphanum.value = numbers[i];
	    toalphanum.baselinepromptsdirectory = directory;
	    var spellprompt = confirmAlphanum( toalphanum );
             
	    for( var j = 0 ; j < spellprompt.audio.length ; j++ )
		{
		    prompts.audio.push( spellprompt.audio[j] );
		    prompts.tts.push( spellprompt.tts[j] );
		}
                
	    if( i < ( numbers.length - 1 ) )
		{
		    prompts.audio.push( directory + "silence500ms.ulaw");
		    prompts.tts.push( " " );
		}
	}
    
    return prompts;
} 

/*******************************************
 * naturalNumber
 *******************************************/
// Produce an array of prompts for a natural number

// es-US: added flag for un/uno switch:
//        0 = uno
//        1 = un

function naturalNumber (confValue, one_switch) {

    // Prompts object
    var prompts = new Object();
    prompts.audio = new Array();
    prompts.tts = new Array();

    //Getting the MEANING key if it is an object
    if ( typeof confValue == 'object' )
	{
	    if (typeof(confValue.MEANING) != 'undefined' && confValue.MEANING != null)
		confValue = confValue.MEANING;
	    else
		confValue = confValue.dm_root;
	}
    var millions = Math.floor(confValue/1000000);
    var thousands = Math.floor((confValue-(millions*1000000))/1000);
    var hundreds = Math.floor((confValue-(millions*1000000)-(thousands*1000))/100);
    var tensOnes = confValue % 100;
    var prompt = 0;

    if (millions > 1) {
	var millionsResult = naturalNumber(millions, 1);
	for ( var i = 0 ; i < millionsResult.audio.length ; i++ ) 
	    {
		prompts.audio.push( millionsResult.audio[i] );
		prompts.tts.push( millionsResult.tts[i] );
	    }
	// push units prompt
	prompts.audio.push( "cardinal/million.ulaw" ); 	// need milliones.ulaw
	prompts.tts.push( "millones" );
    }
    else if(millions == 1) {
	// 1000000 -> "millíon"
	if((thousands == 0) && (hundreds == 1) && (tensOnes == 0)) {
	    prompts.audio.push( "cardinal/million.ulaw" );
	    prompts.tts.push( "millón" );
	}
	else {
	    prompts.audio.push( "cardinal/1.ulaw" );
	    prompts.tts.push( "un" );
	    prompts.audio.push( "cardinal/million.ulaw" );
	    prompts.tts.push( "millón" );
	}    
    }	


    if ( thousands > 1 ) {
	var thousandsResult = naturalNumber(thousands, 1);
	for ( var i=0 ; i < thousandsResult.audio.length ; i++ ) 
	    {
		prompts.audio.push( thousandsResult.audio[i] );
		prompts.tts.push( thousandsResult.tts[i] );
	    }
	// push units prompt
	prompts.audio.push( "cardinal/thousand.ulaw" );
	prompts.tts.push( "mil" );
    }
    else if(thousands == 1) {
	prompts.audio.push( "cardinal/thousand.ulaw" );
	prompts.tts.push( "mil" );
    }	

    // "100" -> cien
    if((hundreds == 1) && (tensOnes == 0)) {
	prompts.audio.push( "cardinal/hundred.ulaw" );
	prompts.tts.push( "cien" );
    }
    
    // otherwise "1xx" -> ciento,...
    else {
	if (hundreds > 0) {
	    prompts.audio.push( "cardinal/" + hundreds + "00.ulaw" );
	    prompts.tts.push( hundredsTTS[Math.round(hundreds)] );
	}
	
	if ((tensOnes > 0) || (millions == 0 && thousands == 0 && hundreds == 0) ){
	    if((tensOnes != 1) || (one_switch == 0)) {
		prompts.audio.push( "cardinal/" + tensOnes + ".ulaw" );
		prompts.tts.push( cardinalTTS[Math.round(tensOnes)] );
	    }
	    else {
		prompts.audio.push( "cardinal/un.ulaw" );
		prompts.tts.push( "un" );
	    }
	}
    }
	
    return prompts;
}


/*************************************************************
 *
 *  Repeat Prompts
 *
 *************************************************************/

function isCommand (args)   {

    var confValue = args.value;

    var isCommand = false;
    if ( typeof confValue == 'object' )
	{
	    if( confValue.dm_command != null )
		isCommand = true;
	}
    return isCommand;
}

/*******************************************
 * repeatAlphanum
 *******************************************/
function repeatAlphanum (args)   {
    
    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmAlphanum(args);

}

/*******************************************
 * repeatCCExpDate
 *******************************************/
function repeatCCExpDate(args){
    
    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmCCExpDate(args);

}

/*******************************************
 * repeatCreditCard
 *******************************************/
function repeatCreditCard( args ){

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmCreditCard(args);

} 

/*******************************************
 * repeatCurrency
 *******************************************/
function repeatCurrency(args){

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmCurrency(args);

}

/*******************************************
 * repeatCustomContext
 *******************************************/
function repeatCustomContext (args)   {
    
    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmCustomContext(args);

}

/*******************************************
 * repeatDate
 *******************************************/
function repeatDate (args)   {
    
    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmDate(args);

}

/*******************************************
 * repeatDigits
 *******************************************/
function repeatDigits (args)   {

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmDigits(args);

}

/*******************************************
 * repeatNaturalNumbers
 *******************************************/
function repeatNaturalNumbers( args ){

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmNaturalNumbers(args);

}

/*******************************************
 * repeatPhone
 *******************************************/
function repeatPhone (args)  {

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmPhone(args);

}

/*******************************************
 * repeatSocialSecurity
 *******************************************/
function repeatSocialSecurity( args ){

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmSocialSecurity(args);

}

/*******************************************
 * repeatTime
 *******************************************/
function repeatTime (args)  {

    if (isCommand(args))
        return confirmCommand(args);
    
    return confirmTime(args);

}

/*******************************************
 * repeatYesNo
 *******************************************/
function repeatYesNo (args)   {
    
    if (isCommand(args))
        return confirmCommand(args);

    return;

}

/*******************************************
 * repeatZipCode
 *******************************************/
function repeatZipCode( args ){

    if (isCommand(args))
        return confirmCommand(args);

    return confirmZipCode(args);

} 
