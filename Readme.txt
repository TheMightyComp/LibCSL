***********************************
*		  		  *
*		LibCSL		  *
*				  *
*	      Jared Bitz	  *
*	     December 2011	  *
*				  *
***********************************

LibCSL is an XNA library written in C# to allow for the easy implementation of cutscenes in XNA-based games.  
Cut-scenes are defined in a .csl file, and parsed in CSLParser.cs.  At run-time, the programmer simply
invokes the parser, and feeds the resulting Scene object to CSLRenderer.